using Domain.Entities.Answers;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IForumDbContext
    {
        DbSet<Post> Posts { get; set; }

        DbSet<Answer> Answers { get; set; }

        DbSet<ApplicationUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
