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
    }
}
