namespace BuildingMarket.Auth.Application.Models.Security
{
    public class RegisterBuyerModel : RegisterModel
    {
        public PreferencesModel Preferences { get; set; }
    }
}
