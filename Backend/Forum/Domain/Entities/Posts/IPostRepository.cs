namespace Domain.Entities.Posts
{
    public interface IPostRepository
    {
        void Add(Post post);
        Task<IEnumerable<Post>> GetAllASync();
        Task<Post?> GetByIdASync(Guid id);
        void DeleteByIdAsync(Guid id);
        void Update(Post post);
    }
}