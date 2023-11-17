using MediatR;

namespace Images.Application.Features.Image.Commands.Delete
{
    public class DeleteCommand : IRequest
    {
        public string DeleteURL { get; set; }
    }
}
