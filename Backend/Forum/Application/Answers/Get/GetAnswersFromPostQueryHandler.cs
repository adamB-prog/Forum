using Application.Data;
using Domain.Entities.Answers;
using Domain.Entities.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Answers.Get
{
    public class GetAnswersFromPostQueryHandler : IRequestHandler<GetAnswersFromPostQuery, ListAnswerResponse>
    {
        private readonly IAnswerRepository _answerRepository;

        public GetAnswersFromPostQueryHandler(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }
        
        public async Task<ListAnswerResponse> Handle(GetAnswersFromPostQuery request, CancellationToken cancellationToken)
        {
            var answers = _answerRepository.GetAllFromPost(request.PostId)
                .Skip(request.Skip)
                .Take(request.Limit)
                .Select(x => new AnswerResponse(
                    x.Id,
                    x.Description,
                    x.CreationTime,
                    x.Owner != null ? x.Owner.UserName : "Deleted"
                    ));


            

            var listOfAnswerResponse = new ListAnswerResponse(answers);


            return listOfAnswerResponse;

        }
    }
}