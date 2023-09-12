using Domain.Primitives;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Posts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Answers
{
    public sealed class Answer : ForumEntity
    {
        public Answer(Guid id, ApplicationUser user, string description) : base(id, user)
        {
            Description = description;
            //LikesFromUsers = new HashSet<ApplicationUser>();
            //DislikesFromUsers = new HashSet<ApplicationUser>();
        }

        public Answer(Guid id, string description) : base(id, null)
        {
            Description = description;
            //LikesFromUsers = new HashSet<ApplicationUser>();
            //DislikesFromUsers = new HashSet<ApplicationUser>();
        }

        public string Description { get; set; }


        //Foreign key to link Answer with Post
        public Guid PostId { get; set; }

        
        public Post? Post { get; set; }

        //Foreign key to owner

        public string? OwnerId { get; set; }


        //public ICollection<ApplicationUser> LikesFromUsers { get; private set; } = new HashSet<ApplicationUser>();

        //public ICollection<ApplicationUser> DislikesFromUsers { get; private set; } = new HashSet<ApplicationUser>();
    }
}