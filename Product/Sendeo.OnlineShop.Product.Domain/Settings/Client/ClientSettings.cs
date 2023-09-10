namespace Sendeo.OnlineShop.Product.Domain.Settings.Client
{
	public class ClientSettings
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public bool AuthorizationEnabled { get; set; } = false;
	}
}
