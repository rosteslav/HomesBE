﻿namespace BuildingMarket.Properties.Application.Models
{
    public class AddPropertyInputModel
    {
        public required string NumberOfRooms { get; set; }
        public required decimal Space { get; set; }
        public string Description { get; set; }
        public required decimal Price { get; set; }
        public required int Floor { get; set; }
        public required int TotalFloorsInBuilding { get; set; }
        public required string BuildingType { get; set; }
        public required string Exposure { get; set; }
        public required string Finish { get; set; }
        public required string Furnishment { get; set; }
        public required string Garage { get; set; }
        public required string Heating { get; set; }
        public required string Neighbourhood { get; set; }
        public string BrokerId { get; set; }
    }
}
