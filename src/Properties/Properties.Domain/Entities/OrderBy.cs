﻿namespace BuildingMarket.Properties.Domain.Entities
{
    public class OrderBy : PropertyInfo
    {
        public bool isAscending = true;

        public string RelatedPropName { get; set; }
    }
}
