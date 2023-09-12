using Application.Data;
using Domain.Entities.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Application.Posts.Get
{
    public sealed class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostResponse>
    {

        private readonly IPostRepository _postRepository;

        public GetPostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostResponse> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository
                .GetByIdASync(request.postId);

            if (post is null)
            {
                //Better exception
                throw new Exception("Not Found");
            }

            var transform = new PostResponse(post.Id, post.Title, post.Description, post.Owner != null ? post.Owner.UserName : "Deleted");

            

            return transform;
        }
    }
}
