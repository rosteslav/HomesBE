using BuildingMarket.Properties.Application.Models.Statics;

namespace BuildingMarket.Properties.Application.Configurations
{
    public class NextTo
    {
        public Dictionary<string, HashSet<string>> Region { get; set; } = new()
        {
            { Regions.South, new() { Regions.West, Regions.Center, Regions.East } },
            { Regions.West, new() { Regions.South, Regions.Center, Regions.North } },
            { Regions.North, new() { Regions.West, Regions.Center, Regions.East } },
            { Regions.East, new() { Regions.South, Regions.Center, Regions.North } },
            { Regions.Center, new() { Regions.South, Regions.West, Regions.North, Regions.East } }
        };

        public Dictionary<string, HashSet<string>> BuildingType { get; set; } = new()
        {
            { BuildingTypes.Brick, new() { BuildingTypes.EPK } },
            { BuildingTypes.EPK, new() { BuildingTypes.Brick } },
            { BuildingTypes.Panel, new() { BuildingTypes.Brick, BuildingTypes.EPK } },
        };

        public Dictionary<string, HashSet<string>> NumberOfRooms { get; set; } = new()
        {
            { Rooms.Studio, new HashSet<string>() { Rooms.OneBedroom, Rooms.Attic } },
            { Rooms.OneBedroom, new HashSet<string>() { Rooms.Studio, Rooms.ThreeBedroom } },
            { Rooms.ThreeBedroom, new HashSet<string>() { Rooms.OneBedroom, Rooms.FourRooms, Rooms.Maisonette } },
            { Rooms.FourRooms, new HashSet<string>() { Rooms.ThreeBedroom, Rooms.Multiroom, Rooms.Maisonette } },
            { Rooms.Multiroom, new HashSet<string>() { Rooms.ThreeBedroom, Rooms.FourRooms, Rooms.Maisonette } },
            { Rooms.Maisonette, new HashSet<string>() { Rooms.ThreeBedroom, Rooms.FourRooms, Rooms.Multiroom } },
            { Rooms.Garage, new HashSet<string>() },
            { Rooms.Warehouse, new HashSet<string>() { Rooms.Garage, Rooms.Attic } },
            { Rooms.Attic, new HashSet<string>() { Rooms.Garage, Rooms.Warehouse } },
        };
    }
}
