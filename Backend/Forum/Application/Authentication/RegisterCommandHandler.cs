using Domain.Entities.ApplicationUsers;
using Domain.Entities.Posts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.viewModel.Email);

            if (user != null) {
                return;
            }

            var createUser = new ApplicationUser
            {
                Email = request.viewModel.Email,
                UserName = request.viewModel.Username,
                Posts = new List<Post>()
            };

            var result = await _userManager.CreateAsync(createUser, request.viewModel.Password);

           
        }
    }
}
