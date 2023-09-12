using Domain.Entities.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public record RegisterCommand(RegisterViewModel viewModel) : IRequest;
    
}
