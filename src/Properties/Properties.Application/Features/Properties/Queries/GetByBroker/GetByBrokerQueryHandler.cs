﻿using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Domain.Entities;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker
{
    public class GetByBrokerQueryHandler(IPropertiesRepository propertiesRepository)
        : IRequestHandler<GetByBrokerQuery, IEnumerable<Property>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;

        public async Task<IEnumerable<Property>> Handle(GetByBrokerQuery request, CancellationToken cancellationToken)
            => await _propertiesRepository.GetByBroker(request.BrokerId);
    }
}
