using Application.Authentication;
using Carter;
using Domain.Entities.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class Auth : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("login", async ([FromBody] LoginViewModel viewModel, ISender sender) =>
            {
                var command = new LoginCommand(viewModel);

                var result = await sender.Send(command);

                if(result != null)
                {
                    return Results.Ok(result);
                }

                return Results.Unauthorized();
            });

            app.MapPost("register", async ([FromBody] RegisterViewModel model, ISender sender) =>
            {
                var command = new RegisterCommand(model);

                //No result yet!
                await sender.Send(command);

                return Results.Ok();
            });
        }
    }
}
