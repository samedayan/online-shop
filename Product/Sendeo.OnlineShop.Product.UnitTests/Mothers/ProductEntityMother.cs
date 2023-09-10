namespace Sendeo.OnlineShop.Product.UnitTests.Mothers
{
	public class ProductEntityMother
	{
		private readonly string[] name = { "Samed", "Emirhan", "Tuğba", "Ramazan", "Sefa", "Gül", "Efe" };
		private readonly string[] lastname = { "Ayan", "Koç", "Demir", "Siyah", "Kırmızı", "Tatlı", "Şeker" };
		private readonly string[] email = { "abc@def.com", "samed@ayan.fyi", "s.ayan@outlook.com", "smdxyn@gmail.com", "smdyn@outlook.com", "samedayan@yandex.com", "isdxy@yandex.com" };
		private readonly string[] phone = { "123456789", "987654321", "00000000", "123123123", "122222333", "555555222", "777777777" };
		private readonly string[] address = { "Bağlıca Mahallesi", "Mermeroğlu Caddesi", "Site Mahallesi", "Kayacan Sokak", "Yeni Mahalle", "Plevne Caddesi", "Osmangazi Mahallesi" };

		public Persistence.PostgreSql.Domain.Product GetProduct()
		{
			return new Persistence.PostgreSql.Domain.Product
			{
				Id = new Random().Next(1, 9999),
				Name = name[new Random().Next(name.Length)],
				Code = lastname[new Random().Next(lastname.Length)],
				Description = address[new Random().Next(lastname.Length)]
			};
		}
	}
}
