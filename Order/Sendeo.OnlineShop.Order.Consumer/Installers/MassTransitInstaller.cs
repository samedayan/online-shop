using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Sendeo.OnlineShop.Order.Consumer.Consumers;
using Sendeo.OnlineShop.Order.Consumer.Observers;
using Sendeo.OnlineShop.Order.Domain.Settings.MassTransit;
using Sendeo.OnlineShop.Order.Infrastructure.Constants;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;

namespace Sendeo.OnlineShop.Order.Consumer.Installers
{
	public static class MassTransitInstaller
	{
		public static void InstallMassTransit(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var busSettings = new BusSettings();
			configuration.GetSection("BusSettings").Bind(busSettings);
			//MassTransit
			// useful documentation https://masstransit-project.com/
			// lots of great example and scenarios https://www.youtube.com/user/PhatBoyG
			serviceCollection.AddMassTransit(x =>
			{
				x.AddConsumer<CreateOrderHandlerConsumer>();
				x.AddConsumer<UpdateOrderHandlerConsumer>();


				// init bus
				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.ConnectConsumeObserver(new ConsumeObserver(new ConsoleLogger()));

					cfg.Host(new Uri($"{busSettings.ClusterAddress}"), h =>
					{
						h.Username(busSettings.UserName);
						h.Password(busSettings.Password);
					});

					cfg.ReceiveEndpoint(QueueNames.CreateOrderHandlerQueueName, ep =>
					{
						ep.AutoDelete = false;

						ep.Durable = true;

						ep.ExchangeType = ExchangeType.Fanout;

						ep.UseMessageRetry(r => { r.Interval(3, TimeSpan.FromMilliseconds(1000)); });

						ep.PrefetchCount = 10;

						ep.UseKillSwitch(options => options
							.SetActivationThreshold(10)
							.SetTripThreshold(0.15)
							.SetRestartTimeout(m: 1));

						ep.ConfigureConsumer<CreateOrderHandlerConsumer>(context);
					});

					cfg.ReceiveEndpoint(QueueNames.UpdateOrderHandlerQueueName, ep =>
					{
						ep.AutoDelete = false;

						ep.Durable = true;

						ep.ExchangeType = ExchangeType.Fanout;

						ep.UseMessageRetry(r => { r.Interval(3, TimeSpan.FromMilliseconds(1000)); });

						ep.PrefetchCount = 10;

						ep.UseKillSwitch(options => options
							.SetActivationThreshold(10)
							.SetTripThreshold(0.15)
							.SetRestartTimeout(m: 1));

						ep.ConfigureConsumer<UpdateOrderHandlerConsumer>(context);
					});
				});
			});
		}
	}
}
