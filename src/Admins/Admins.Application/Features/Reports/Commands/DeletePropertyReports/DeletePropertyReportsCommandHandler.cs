using BuildingMarket.Admins.Application.Contracts;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Reports.Commands.DeletePropertyReports
{
    public class DeletePropertyReportsCommandHandler(IReportsStore reportsStore) : IRequestHandler<DeletePropertyReportsCommand>
    {
        private readonly IReportsStore _reportsStore = reportsStore;

        public async Task Handle(DeletePropertyReportsCommand request, CancellationToken cancellationToken)
            => await _reportsStore.DeletePropertyReports(request.PropertyId, cancellationToken);
    }
}
