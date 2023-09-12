using Application.Posts.Get;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Answers.Get
{
    public record GetAnswersFromPostQuery(Guid PostId, int Skip, int Limit) : IRequest<ListAnswerResponse>;

    public record ListAnswerResponse(IEnumerable<AnswerResponse> AnswerResponses);
    
}
