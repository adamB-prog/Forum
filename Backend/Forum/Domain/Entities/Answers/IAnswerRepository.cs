namespace Domain.Entities.Answers
{
    public interface IAnswerRepository
    {
        void Add(Answer answer);
        void Delete(Guid id);
        void Update(Answer answer);

        IEnumerable<Answer> GetAllFromPost(Guid postId);
    }
}