using Application.Data;
using Domain.Entities.Answers;
using Domain.Entities.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Answers.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommandHandler(IAnswerRepository answerRepository, IUnitOfWork unitOfWork)
        {
            _answerRepository = answerRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            _answerRepository.Delete(request.id);

            _unitOfWork.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
