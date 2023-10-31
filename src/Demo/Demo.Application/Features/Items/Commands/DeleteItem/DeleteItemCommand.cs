using MediatR;

namespace Demo.Application.Features.Items.Commands.DeleteItem
{
    public class DeleteItemCommand : IRequest
    {
        public int Id { get; set; }
    }
}
