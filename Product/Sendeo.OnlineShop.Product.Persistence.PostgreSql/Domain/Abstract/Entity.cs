using Sendeo.OnlineShop.Product.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Product.Persistence.PostgreSql.Domain.Abstract
{
	public abstract class Entity : IEntity<int>, IAuditableEntity
	{
		public AuditInformation AuditInformation { get; set; }

		public int Id { get; }

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj.GetType().IsAssignableFrom(typeof(Entity)) && Equals((Entity)obj);
		}

		public bool Equals(Entity other)
		{
			return Id.Equals(other.Id);
		}

		public int CompareTo(Entity other)
		{
			return Id.CompareTo(other.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity a, Entity b)
		{
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(Entity a, Entity b)
		{
			return !(a == b);
		}
	}
}
