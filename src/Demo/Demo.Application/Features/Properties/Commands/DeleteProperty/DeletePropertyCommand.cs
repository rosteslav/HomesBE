using MediatR;

namespace Demo.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommand : IRequest
    {
        public int Id { get; set; }
    }
}
