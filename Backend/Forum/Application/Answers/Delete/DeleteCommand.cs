using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Answers.Delete;

public record DeleteCommand(Guid id) : IRequest;


