using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Answers.Get
{
    public record AnswerResponse(Guid Id, string Description, DateTime CreationTime, string OwnerUsername);
}
