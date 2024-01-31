﻿using BuildingMarket.Properties.Application.Contracts;
using BuildingMarket.Properties.Application.Models;
using MediatR;

namespace BuildingMarket.Properties.Application.Features.Properties.Queries.GetByBroker
{
    public class GetByBrokerQueryHandler(IPropertiesRepository propertiesRepository, IPropertyImagesStore propertyImagesStore)
        : IRequestHandler<GetByBrokerQuery, IEnumerable<PropertyModelWithId>>
    {
        private readonly IPropertiesRepository _propertiesRepository = propertiesRepository;
        private readonly IPropertyImagesStore _propertyImagesStore = propertyImagesStore;

        public async Task<IEnumerable<PropertyModelWithId>> Handle(GetByBrokerQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertiesRepository.GetByBroker(request.BrokerId, cancellationToken);
            if (properties.Any())
            {
                var propertiesIds = properties.Select(p => p.Id.ToString()).ToArray();
                var propertiesImages = await _propertyImagesStore.GetPropertiesImages(propertiesIds);
                if (propertiesImages.Any())
                {
                    for (int i = 0; i < propertiesImages.Count(); i++)
                        properties.ElementAt(i).Images = propertiesImages.ElementAt(i).Images.Distinct();
                }
            }

            return properties;
        }
    }
}
