using Application.Data;
using Domain.Entities.Answers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class AnswerRepository : IAnswerRepository
    {
        private IForumDbContext _forumDbContext;

        public AnswerRepository(IForumDbContext context)
        {
            _forumDbContext = context;
        }

        public async Task<Answer?> GetByIdASync(Guid id)
        {
            var answer = await _forumDbContext.Answers
                .SingleOrDefaultAsync(x => x.Id == id);

            return answer;
        }
        public async Task<IEnumerable<Answer>> GetAllASync()
        {
            var all = await _forumDbContext.Answers.ToListAsync();

            return all;
        }

        public void Add(Answer answer)
        {
            _forumDbContext.Answers.Add(answer);

        }

        public async void Delete(Guid id)
        {
            var needToRemove = await GetByIdASync(id);

            if (needToRemove is null)
            {
                return;
            }

            _forumDbContext.Answers.Remove(needToRemove);

        }


        public async void Update(Answer answer)
        {
            var needToUpdate = await GetByIdASync(answer.Id);

            if (needToUpdate is null)
            {
                return;
            }
            needToUpdate.Description = answer.Description;

        }

        public IEnumerable<Answer> GetAllFromPost(Guid postId)
        {
            var result = _forumDbContext.Answers.Where(x => x.PostId == postId);

            return result;
        }

    }
}
