using Application.Data;
using Domain.Entities.Answers;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>, 
        IForumDbContext, 
        IUnitOfWork
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public ForumDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);
        }

        
    }
}
