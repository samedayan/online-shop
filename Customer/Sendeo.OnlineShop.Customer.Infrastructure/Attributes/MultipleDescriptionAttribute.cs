namespace Sendeo.OnlineShop.Customer.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class MultipleDescriptionAttribute : Attribute
	{
		public MultipleDescriptionAttribute(params string[] description)
		{
			Description = description;
		}

		public string[] Description { get; set; }
	}
}
