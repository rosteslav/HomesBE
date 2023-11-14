namespace BuildingMarket.Auth.Domain.Entities
{
    public class AdditionalUserData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
    }
}
