using Domain.Entities.Answers;
using MediatR;

namespace Application.Posts.Get;

public record GetPostQuery(Guid postId) : IRequest<PostResponse>;

public record PostResponse(Guid Id,
        string Title,
        string Description,
        string Ownerusername);
