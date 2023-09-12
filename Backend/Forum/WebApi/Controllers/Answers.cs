using Application.Answers.Create;
using Application.Answers.Delete;
using Application.Answers.Get;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class Answers : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("answers", async ([FromBody] CreateAnswerCommand command, ISender sender) =>
            {
                await sender.Send(command);

                return Results.Ok();
            });

            app.MapGet("answers/{postId:Guid}/{skip:int}/{limit:int}", async (Guid postId, int skip, int limit, ISender sender) =>
            {
                var query = new GetAnswersFromPostQuery(postId, skip, limit);

                var result = await sender.Send(query);

                return Results.Ok(result);

            });

            app.MapDelete("answers/{answerId:Guid}", async (Guid answerId, ISender sender) =>
            {
                var command = new DeleteCommand(answerId);

                await sender.Send(command);

                return Results.NoContent();
            });
        }
    }
}
