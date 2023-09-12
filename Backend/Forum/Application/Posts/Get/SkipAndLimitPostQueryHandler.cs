using Application.Data;
using Domain.Entities.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Get
{
    public class SkipAndLimitPostQueryHandler : IRequestHandler<SkipAndLimitPostQuery, SkipAndLimitPostResponse>
    {

        private readonly IPostRepository _postRepository;

        public SkipAndLimitPostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<SkipAndLimitPostResponse> Handle(SkipAndLimitPostQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllASync();

            var transform = posts.Select(p => new PostResponse(
                    p.Id, 
                    p.Title, 
                    p.Description,
                    p.Owner != null ? p.Owner.UserName : "Deleted"
                    ))
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToList();

            var response = new SkipAndLimitPostResponse(transform, request.Skip, request.Limit);

            return response;
        }
    }
}