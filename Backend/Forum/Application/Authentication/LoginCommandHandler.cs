using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, TokenModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<TokenModel?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.model.Username);

            if (user == null)
            {
                return null;
            }

            if (!await _userManager.CheckPasswordAsync(user, request.model.Password))
            {
                return null;
            }

            var claim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.NameId, user.NormalizedUserName),
                };
            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MUSTCHANGEITLATER"));
            var token = new JwtSecurityToken(
                    issuer: "http://www.security.org", audience: "http://www.security.org",
                    claims: claim, expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
            //Send only the token not the entire thing

            return new TokenModel(
                new JwtSecurityTokenHandler().WriteToken(token),
                token.ValidTo
                );
        }
    }
}
