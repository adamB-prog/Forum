using Application.Posts.Create;
using Application.Posts.Get;
using Carter;
using Domain.Entities.Posts;
using MediatR;

namespace WebApi.Controllers;

public class Posts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("posts/{id:guid}", async (Guid id, ISender sender) =>
       {
            try
            {
                var query = new GetPostQuery(id);

                var response = await sender.Send(query);
                

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapGet("posts/{skip:int}/{limit:int}", async (int skip, int limit, ISender sender) =>
        {
            var query = new SkipAndLimitPostQuery(skip, limit);

            var response = await sender.Send(query);

            return Results.Ok(response);
        });

        app.MapPost("posts", async (string title, string description, ISender sender) =>
        {
            var command = new CreatePostCommand(title, description, DateTime.Now);

            await sender.Send(command);

            return Results.Ok();
        });
    }
}
