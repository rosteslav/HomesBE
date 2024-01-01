using BuildingMarket.Common.Models;

namespace BuildingMarket.Admins.Application.Models
{
    public class AllReportsModel
    {
        public int PropertyId { get; set; }

        public List<ReportRedisModel> Reports { get; set; }
    }
}
