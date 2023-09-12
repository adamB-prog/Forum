using Microsoft.AspNetCore.Identity;
using Domain.Entities.Posts;
using Domain.Entities.Answers;

namespace Domain.Entities.ApplicationUsers
{
    public sealed class ApplicationUser : IdentityUser
    {
        public ICollection<Post>? Posts { get; set; }

        public ICollection<Answer>? Answers { get; set; }

        //public ICollection<Answer>? LikedAnswers { get; set; }

        //public ICollection<Answer>? DislikedAnswers { get; set; }

        //public ICollection<Post>? LikedPosts { get; set; }

        //public ICollection<Post>? DislikedPosts { get; set; }
    }
}
