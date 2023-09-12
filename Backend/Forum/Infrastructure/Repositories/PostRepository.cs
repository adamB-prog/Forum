using Application.Data;
using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class PostRepository : IPostRepository
    {
        private readonly IForumDbContext _forumDbContext;

        public PostRepository(IForumDbContext context)
        {
            _forumDbContext = context;
        }

        public async Task<Post?> GetByIdASync(Guid id)
        {
            var post = await _forumDbContext.Posts
                .SingleOrDefaultAsync(p => p.Id == id);

            return post;
        }

        public async Task<IEnumerable<Post>> GetAllASync()
        {
            var all = await _forumDbContext.Posts.ToListAsync();

            return all;
        }

        public void Add(Post post)
        {
            _forumDbContext.Posts.Add(post);
        }

        public async void DeleteByIdAsync(Guid id)
        {
            var needToRemove = await GetByIdASync(id);

            if (needToRemove is null)
            {
                return;
            }

            _forumDbContext.Posts.Remove(needToRemove);

        }


        public async void Update(Post post)
        {
            var needToUpdate = await GetByIdASync(post.Id);

            if (needToUpdate is null)
            {
                return;
            }
            needToUpdate.Description = post.Description;

            needToUpdate.Title = post.Title;

        }
    }
}
