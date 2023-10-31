using MediatR;

namespace Demo.Application.Features.Items.Commands.AddItem
{
    public class AddItemCommand : IRequest
    {
        public string Name { get; set; }
    }
}
