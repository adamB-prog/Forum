using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Answers.Create
{
    public record CreateAnswerCommand(string Description, Guid PostId) : IRequest;
}
