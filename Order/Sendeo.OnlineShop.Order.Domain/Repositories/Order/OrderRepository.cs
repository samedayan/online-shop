using Microsoft.EntityFrameworkCore;
using Sendeo.OnlineShop.Order.Domain.Extensions;
using Sendeo.OnlineShop.Order.Persistence.PostgreSql.DataAccess;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;
using System.Linq.Expressions;

namespace Sendeo.OnlineShop.Order.Domain.Repositories.Order
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IDbContextFactory<OrderDatabaseContext> _dbContextFactory;

		public OrderRepository(IDbContextFactory<OrderDatabaseContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
		}

		public (int totalCount, IReadOnlyCollection<Persistence.PostgreSql.Domain.Order>) GetOrders(GetOrderQuery request)
		{
			Expression<Func<Persistence.PostgreSql.Domain.Order, bool>> predicate = x => true;

			if (request.StartDate is not null)
			{
				predicate = predicate.And(s => s.AuditInformation.CreatedDate >= request.StartDate);
			}
			
			if (request.EndDate is not null)
			{
				predicate = predicate.And(s => s.AuditInformation.CreatedDate <= request.EndDate);
			}
			
			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.Order				
				.Where(predicate)
				.OrderByDescending(s => s.Id)
				.AsNoTracking();

			var count = query.Count();

			if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

			if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);

			return (count, query.ToList());
		}

		public Persistence.PostgreSql.Domain.Order? GetOrderById(GetOrderByIdQuery request)
		{
			Expression<Func<Persistence.PostgreSql.Domain.Order, bool>> predicate = x => true;

			predicate = predicate.And(s => s.Id == request.Id);

			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.Order.Where(predicate).AsNoTracking();

			return query.FirstOrDefault();
		}

		public async Task<bool> CreateOrderAsync(Persistence.PostgreSql.Domain.Order product)
		{			
			using var dbContext = _dbContextFactory.CreateDbContext();

			await dbContext.Order.AddAsync(product);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> UpdateOrderAsync(Persistence.PostgreSql.Domain.Order product)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteOrderAsync(Persistence.PostgreSql.Domain.Order product)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			dbContext.Order.Remove(product);

			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
