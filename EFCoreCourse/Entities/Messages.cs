using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class Messages: Entity
    {
        #region Properties

        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public Guid SenderId { get; set; }
        public Person Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public Person Receiver { get; set; }

        #endregion

        #region Methods

        public static Messages Create(Guid senderId, Guid receiverId, string text)
        {
            if (senderId == receiverId)
                throw new Exception("Sender and Receiver cannot be the same person.");
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("The message text cannot be empty.");
            
            return new Messages
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Text = text,
                SentAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };
        }

        #endregion
    }
}
