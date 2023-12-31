using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.ReportProperty
{
    public class ReportPropertyCommand : IRequest
    {
        public string UserId { get; set; }

        public int PropertyId { get; set; }

        public ReportModel ReportModel { get; set; }
    }
}
