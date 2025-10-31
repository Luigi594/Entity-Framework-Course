using EFCoreCourse.Entities;
using EFCoreCourse.Entities.Enums;
using EFCoreCourse.Server.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFCoreCourse.Server.Cruds
{
    public class MovieRentalCrudController
    {
        public class CreateMovieRent
        {
            public class CreateMovieRentCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public Guid CustomerId { get; set; }
                public PaymentVm Payment { get; set; }
                public List<MovieRentalVm> MovieRentals { get; set; }

                public class MovieRentalVm
                {
                    public DateTime RentalDate { get; set; }
                    public DateTime? ReturnDate { get; set; }
                    public decimal RentalPrice { get; set; }
                    public Guid MovieId { get; set; }
                }

                public class PaymentVm
                {
                    public PaymentType PaymentType { get; set; }
                    public decimal Amount { get; set; }
                    public PayPalPayment.Vm PayPal { get; set; }
                    public CreditCardPayment.Vm CreditCard { get; set; }
                    public CashPayment.Vm Cash { get; set; }
                }
            }

            public class Validator : AbstractValidator<CreateMovieRentCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Payment.Amount).NotEmpty().WithMessage("Amount is required");

                    //RuleFor(x => x.Payment.CreditCard.CardNumber)
                    //    .MaximumLength(4).WithMessage("Card Number must be at least 4 digits");

                    //RuleFor(x => x.Payment.CreditCard.CardHolderName)
                    //    .MaximumLength(100).WithMessage("Card holder name must not exceed 100 characters.");

                    //RuleFor(x => x.Payment.PayPal.Email)
                    //    .Must(EmailValidator.IsValidEmail).WithMessage("The Email is not valid!");

                }
            }

            public class CreateMovieRentCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateMovieRentCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateMovieRentCommand command, CancellationToken cancellationToken)
                {
                    #region Queries and Validations 

                    var customer = await _context.Person.OfType<Customer>()
                        .FirstOrDefaultAsync(x => x.Id == command.CustomerId, cancellationToken)
                        ?? throw new Exception("Customer not found.");

                    var moviesIds = command.MovieRentals.Select(x => x.MovieId).Distinct().ToList();
                    var movies = await _context.Movie.Where(x => moviesIds.Contains(x.Id)).ToListAsync(cancellationToken);

                    for (int i = 0; i < movies.Count; i++)
                    {
                        var movie = movies.ElementAt(i);
                        var existe = movies.Where(x => x.Id == movie.Id).FirstOrDefault()
                            ?? throw new Exception($"Movie with Id {movie.Id} was not found.");

                        if (!movie.IsAvailableForRental)
                            throw new Exception($"Movie '{movie.Title}' is not available for rental.");

                        if (movie.AvailableCopies <= 0)
                            throw new Exception($"Movie '{movie.Title}' does not have available copies for rental.");
                    }

                    if (!Enum.IsDefined(command.Payment.PaymentType))
                        throw new Exception($"Invalid payment type.");

                    if (command.MovieRentals.Count == 0)
                        throw new Exception("You need to include at least one movie.");


                    foreach (var rental in command.MovieRentals)
                    {
                        if (rental.ReturnDate.HasValue && rental.ReturnDate < rental.RentalDate)
                        {
                            throw new Exception("The return date cannot be earlier than the rental date.");
                        }
                    }

                    var totalRentalPrice = command.MovieRentals.Sum(x => x.RentalPrice);
                    if (command.Payment.Amount < totalRentalPrice)
                        throw new Exception($"Total amount {command.Payment.Amount} does not match the total rental price: {totalRentalPrice}.");

                    #endregion

                    #region Payment Switch

                    Payment payment;
                    switch (command.Payment.PaymentType)
                    {
                        case PaymentType.Cash:
                            var newPaymentCash = CashPayment.Create(command.Payment.Cash.ReceivedBy, totalRentalPrice);

                            if (command.Payment.Cash.ChangeGiven)
                            {
                                if (command.Payment.Amount < totalRentalPrice)
                                {
                                    throw new Exception($@"The amount received cannot be less than the total income.
                                    The total amount is: {totalRentalPrice} and the user is paying: {command.Payment.Amount}.");
                                }

                                newPaymentCash.ChangeGiven = true;
                                newPaymentCash.ChangeAmount = command.Payment.Amount - totalRentalPrice;
                                newPaymentCash.Amount = command.Payment.Amount;
                            }

                            payment = newPaymentCash;
                            break;


                        case PaymentType.CreditCard:

                            var newPaymentCreditCard = CreditCardPayment.Create(command.Payment.CreditCard.CardNumber, command.Payment.CreditCard.CardHolderName, totalRentalPrice);
                            payment = newPaymentCreditCard;
                            break;


                        case PaymentType.PayPal:

                            var newPaymentPayPal = PayPalPayment.Create(command.Payment.PayPal.Email, totalRentalPrice);
                            payment = newPaymentPayPal;
                            break;

                        default:
                            throw new Exception("Payment type not supported.");
                    }

                    await _context.Payment.AddAsync(payment, cancellationToken);

                    #endregion

                    var newRentalTransaction = RentalTransaction.Create(customer.Id, payment.Id);

                    await _context.AddAsync(newRentalTransaction, cancellationToken);

                    foreach (var rentalVm in command.MovieRentals)
                    {
                        var movie = movies.Where(x => x.Id == rentalVm.MovieId).FirstOrDefault()
                            ?? throw new Exception($"Movie with Id {rentalVm.MovieId} was not found.");

                        if (movie.AvailableCopies == 0)
                        {
                            movie.IsAvailableForRental = false;
                            throw new Exception($"We apologize, but the movie {movie.Title} is no longer available.");
                        }

                        // Reduce available copies
                        movie.AvailableCopies--;

                        var newMovieRental = MovieRental.Create(newRentalTransaction.Id, rentalVm.MovieId,
                            rentalVm.RentalDate, rentalVm.RentalPrice);

                        if (rentalVm.ReturnDate.HasValue)
                            newMovieRental.ReturnDate = rentalVm.ReturnDate;

                        newRentalTransaction.MovieRentals.Add(newMovieRental);
                    }

                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);

                        return EndpointResponses.ResponseWithSimpleMessage
                            .Create($"Your purchase was successful with reference: {newRentalTransaction.ReferenceCode}");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        await transaction.RollbackAsync(cancellationToken);

                        var failedEntry = ex.Entries.FirstOrDefault();

                        // We say that tha failed entry is Movie because it has the AvailableCopies property
                        // that can be updated by multiple users at the same time
                        // For example, two users trying to rent the last available copy of a movie
                        // If that fails we revert the transaction and notify the user
                        if (failedEntry?.Entity is Movie failedMovie)
                        {
                            throw new Exception($"The movie '{failedMovie.Title}' was just rented by another user. " +
                                $"Please review the available copies and try again.");
                        }

                        throw new Exception("Unexpected concurrency error.The data was modified by another user. " +
                            "The transaction has been canceled.");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        throw;
                    }
                }
            }
        }
    }
}
