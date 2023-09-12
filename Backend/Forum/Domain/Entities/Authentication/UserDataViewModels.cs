namespace Domain.Entities.Authentication
{
    public record LoginViewModel(string Username, string Password);

    public record RegisterViewModel(string Username, string Email, string Password);

}
