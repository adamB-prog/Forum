using System.IdentityModel.Tokens.Jwt;

namespace Application.Authentication
{
    public record TokenModel(string token, DateTime expiration);
}