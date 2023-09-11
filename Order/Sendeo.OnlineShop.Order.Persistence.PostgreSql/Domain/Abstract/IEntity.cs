﻿namespace Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain.Abstract
{
	public interface IEntity<out T> where T : struct, IComparable<T>, IEquatable<T>
	{
		public T Id { get; }
	}
}
