using BuildingMarket.Properties.Application.Contracts;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Commands.ReportProperty
{
    public class ReportPropertyCommandHandler(IPropertiesStore store) : IRequestHandler<ReportPropertyCommand>
    {
        private readonly IPropertiesStore _store = store;

        public async Task Handle(ReportPropertyCommand request, CancellationToken cancellationToken)
            => await _store.UploadReport(request, cancellationToken);
    }
}
