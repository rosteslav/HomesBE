using MediatR;

namespace BuildingMarket.Admins.Application.Features.Reports.Commands.DeletePropertyReports
{
    public class DeletePropertyReportsCommand : IRequest
    {
        public int PropertyId { get; set; }
    }
}
