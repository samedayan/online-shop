﻿namespace Sendeo.OnlineShop.Order.Domain.Settings.MassTransit
{
	public record BusSettings
	{
		public string ClusterAddress { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
