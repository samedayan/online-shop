using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Domain;

namespace Sendeo.OnlineShop.Customer.UnitTests.Mothers
{
	public class UserEntityMother
	{
		private readonly string[] name = { "Samed", "Emirhan", "Tuğba", "Ramazan", "Sefa", "Gül", "Efe" };
		private readonly string[] lastname = { "Ayan", "Koç", "Demir", "Siyah", "Kırmızı", "Tatlı", "Şeker" };
		private readonly string[] email = { "abc@def.com", "samed@ayan.fyi", "s.ayan@outlook.com", "smdxyn@gmail.com", "smdyn@outlook.com", "samedayan@yandex.com", "isdxy@yandex.com" };
		private readonly string[] phone = { "123456789", "987654321", "00000000", "123123123", "122222333", "555555222", "777777777" };
		private readonly string[] address = { "Bağlıca Mahallesi", "Mermeroğlu Caddesi", "Site Mahallesi", "Kayacan Sokak", "Yeni Mahalle", "Plevne Caddesi", "Osmangazi Mahallesi" };

		public User GetUser()
		{
			return new User
			{
				Id = new Random().Next(1, 9999),
				Name = name[new Random().Next(name.Length)],
				LastName = lastname[new Random().Next(lastname.Length)],
				Email = email[new Random().Next(email.Length)],
				Phone = phone[new Random().Next(phone.Length)],
				Address = address[new Random().Next(lastname.Length)]
			};
		}
	}
}
