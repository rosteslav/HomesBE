﻿using BuildingMarket.Properties.Application.Features.Properties.Commands.ReportProperty;
using BuildingMarket.Properties.Application.Models;

namespace BuildingMarket.Properties.Application.Contracts
{
    public interface IPropertiesStore
    {
        Task UploadProperties(IDictionary<int, PropertyRedisModel> properties, CancellationToken cancellationToken);

        Task UploadReport(ReportPropertyCommand model, CancellationToken cancellationToken);
    }
}