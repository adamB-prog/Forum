using Application.Data;
using Domain.Entities.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostRepository _postRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            
            var post = new Post(
                Guid.NewGuid(),
                request.Title,
                request.Description
                );

            _postRepository.Add(post);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
