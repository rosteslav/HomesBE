﻿using StackExchange.Redis;

namespace BuildingMarket.Common.Providers.Interfaces
{
    public interface IRedisProvider
    {
        IDatabase GetDatabase();
    }
}
