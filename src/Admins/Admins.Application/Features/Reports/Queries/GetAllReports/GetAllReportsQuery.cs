﻿using BuildingMarket.Admins.Application.Models;
using MediatR;

namespace BuildingMarket.Admins.Application.Features.Reports.Queries.GetAllReports
{
    public class GetAllReportsQuery : IRequest<List<AllReportsModel>>
    {
    }
}
