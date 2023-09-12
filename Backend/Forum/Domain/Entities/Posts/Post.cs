using Domain.Primitives;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Answers;

namespace Domain.Entities.Posts
{
    public sealed class Post : ForumEntity
    {
        public Post(Guid id, ApplicationUser user, string title, string description) : base(id, user)
        {
            Title = title;
            Description = description;
        }

        public Post(Guid id, string title, string description) : base(id, null)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }

        public string Description { get; set; }


        //Foreign key to link Post with owner
        public string? OwnerId { get; set; }



        //One to Many relation with Answers
        public ICollection<Answer>? Answers { get; private set; } = new HashSet<Answer>();

        //public ICollection<ApplicationUser> LikesOnPostsByUser { get; private set; }

        //public ICollection<ApplicationUser> DislikesOnPostsByUser { get; private set; }
    }
}
