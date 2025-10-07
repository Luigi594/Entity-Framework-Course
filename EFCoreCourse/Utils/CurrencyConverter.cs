using EFCoreCourse.Entities.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreCourse.Utils
{
    public class CurrencyConverter: ValueConverter<Currency, string>
    {
        public CurrencyConverter() : base(
                value => ConvertToProvider(value),
                value => ConvertFromProvider(value)
            ) 
        {

        }

        private static new string ConvertToProvider(Currency currency) =>
            currency switch
            {
                Currency.Usd => "USD",
                Currency.Eur => "EUR",
                Currency.Gbp => "GBP",
                Currency.Rdd => "RDD",
                _ => "",
            };

        private static new Currency ConvertFromProvider(string currency) =>
            currency.ToUpper() switch
            {
                "USD" => Currency.Usd,
                "EUR" => Currency.Eur,
                "GBP" => Currency.Gbp,
                "RDD" => Currency.Rdd,
                _ => Currency.Unknown,
            };
    }
}
