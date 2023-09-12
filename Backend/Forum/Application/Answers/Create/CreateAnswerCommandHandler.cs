using Application.Data;
using Domain.Entities.Answers;
using Domain.Entities.Posts;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Answers.Create
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand>
    {
        private readonly IAnswerRepository _answerRepository;

        private readonly IPostRepository _postRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CreateAnswerCommandHandler(IAnswerRepository answerRepository, IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _answerRepository = answerRepository;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new Answer(Guid.NewGuid(), request.Description);
            var ownerPost = await _postRepository.GetByIdASync(request.PostId);


            ownerPost.Answers.Add(answer);

            _answerRepository.Add(answer);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
