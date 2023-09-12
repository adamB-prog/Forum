using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Create
{
    public record CreatePostCommand(string Title, 
        string Description,
        DateTime CreationTime
        ): IRequest;
    
}
