using Domain.Entities.ApplicationUsers;

namespace Domain.Primitives
{
    public class ForumEntity : Entity
    {
        public ForumEntity(Guid id, ApplicationUser? user) : base(id)
        {
            Owner = user;
            CreationTime = DateTime.Now;
        }

        public ApplicationUser? Owner { get; private set; }

        public DateTime CreationTime { get; private set; }
    }
}
