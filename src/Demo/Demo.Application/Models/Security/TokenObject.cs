namespace Demo.Application.Models.Security
{
    public class TokenObject
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
