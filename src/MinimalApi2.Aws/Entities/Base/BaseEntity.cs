using System.ComponentModel.DataAnnotations;

namespace MinimalApi2.Aws.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ChangedAt { get; private set; }
        public bool IsActive { get; private set; } = true;


        protected void AssignId()
        {
            Id = Guid.NewGuid();
        }

        protected void AssignId(Guid id)
        {
            Id = id;
        }



        protected void AssignCreatedAt()
        {
            CreatedAt = DateTime.UtcNow;
        }

        protected void AssignCreatedAt(DateTime dateTime)
        {
            CreatedAt = dateTime;
        }



        protected void AssignChangedAt()
        {
            ChangedAt = DateTime.UtcNow;
        }

        protected void AssignChangedAt(DateTime dateTime)
        {
            ChangedAt = dateTime;
        }



        protected void AssignIsActive()
        {
            IsActive = true;
        }

        protected void AssignIsActive(bool isActive)
        {
            IsActive = isActive;
        }

    }
}
