using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetRecommended
{
    public class GetRecommendedQueryHandler(IPropertiesRepository repository) :
        IRequestHandler<GetRecommendedQuery, IEnumerable<GetAllPropertiesOutputModel>>
    {
        private readonly IPropertiesRepository _repository = repository;

        public async Task<IEnumerable<GetAllPropertiesOutputModel>> Handle(
            GetRecommendedQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetRecommended(cancellationToken);
        }
    }
}
