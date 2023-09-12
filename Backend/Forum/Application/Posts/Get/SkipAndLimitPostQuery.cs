using Domain.Entities.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Get
{
    public record SkipAndLimitPostQuery(int Skip, int Limit) : IRequest<SkipAndLimitPostResponse>;


    public record SkipAndLimitPostResponse(IEnumerable<PostResponse> Posts, int Skip, int Limit);
}
