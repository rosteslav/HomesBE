using BuildingMarket.Admins.Application.Contracts;
using BuildingMarket.Admins.Application.Models;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Reports.Queries.GetAllReports
{
    public class GetAllReportsQueryHandler(IReportsStore reportsStore)
        : IRequestHandler<GetAllReportsQuery, List<AllReportsModel>>
    {
        private readonly IReportsStore _reportsStore = reportsStore;

        public async Task<List<AllReportsModel>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
            => await _reportsStore.GetAllReports(cancellationToken);
    }
}
