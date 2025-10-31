namespace EFCoreCourse.Entities
{
    public abstract class Entity
    {
        // If we wanted to have common properties for all entities, we could define them here.
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        //public Guid CreatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public Guid? UpdatedBy { get; set; }

        public bool IsSoftDeleted { get; set; } = false;
        public byte[] RowVersion { get; set; }

        // We could also define common methods for all entities here, if needed.
        public bool SoftDelete()
        {
            if (IsSoftDeleted) return false;
            IsSoftDeleted = true;
            return true;
        }

        public void UndoSoftDelete()
        {
            if (IsSoftDeleted) IsSoftDeleted = false;
        }
    }
}
