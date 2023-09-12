using Domain.Entities.Authentication;
using MediatR;

namespace Application.Authentication
{
    public record LoginCommand(LoginViewModel model) : IRequest<TokenModel>;
}
