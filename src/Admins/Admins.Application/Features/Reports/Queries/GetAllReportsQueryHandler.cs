using BuildingMarket.Admins.Application.Contracts;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Reports.Queries
{
    public class GetAllReportsQueryHandler(IReportsStore reportsStore)
        : IRequestHandler<GetAllReportsQuery>
    {
        private readonly IReportsStore _reportsStore = reportsStore;

        public Task Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
            => _reportsStore.GetAllReports(cancellationToken);
    }
}
