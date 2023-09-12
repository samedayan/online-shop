using Microsoft.EntityFrameworkCore;
using Sendeo.OnlineShop.Order.Domain.Extensions;
using Sendeo.OnlineShop.Order.Persistence.PostgreSql.DataAccess;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using System.Linq.Expressions;
using Sendeo.OnlineShop.Order.Infrastructure.Exceptions;

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
				.Include(s => s.OrderProducts)
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

			var query = dbContext.Order.Include(s => s.OrderProducts).Where(predicate).AsNoTracking();

			return query.FirstOrDefault();
		}

		public async Task<bool> CreateOrderAsync(Persistence.PostgreSql.Domain.Order request)
		{			
			using var dbContext = _dbContextFactory.CreateDbContext();

			await dbContext.Order.AddAsync(request);

			await dbContext.SaveChangesAsync();

			return true;
		}
		
		public async Task<bool> UpdateOrderAsync(Persistence.PostgreSql.Domain.Order request)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			var order = GetOrderById(new GetOrderByIdQuery{ Id = request.Id});
			
			if (order is null)
			{
				throw new BusinessException("Order Not Found!.", ExceptionCodes.DefaultExceptionCode);
			}

			order.CustomerId = request.CustomerId;
			order.Description = request.Description;
			order.StatusId = request.StatusId;
			order.AuditInformation.LastModifiedDate = DateTime.Now.ToUniversalTime();
			
			foreach (var orderProduct in order.OrderProducts)
			{
				var item = request.OrderProducts.FirstOrDefault(s => s.Id == orderProduct.Id);

				if (item is null)
				{
					continue;
				}

				orderProduct.AuditInformation.LastModifiedDate = DateTime.Now.ToUniversalTime();
				orderProduct.ProductId = item.ProductId;
				orderProduct.OrderId = item.OrderId;
				orderProduct.Description = item.Description;
				orderProduct.Quantity = item.Quantity;
			}

			dbContext.Update(order);
			
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteOrderAsync(Persistence.PostgreSql.Domain.Order request)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			dbContext.Order.Remove(request);

			await dbContext.SaveChangesAsync();

			return true;
		}
		
		public Persistence.PostgreSql.Domain.OrderProduct? GetOrderProductById(int id)
		{
			Expression<Func<Persistence.PostgreSql.Domain.OrderProduct, bool>> predicate = x => true;

			predicate = predicate.And(s => s.Id == id);

			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.OrderProduct.Where(predicate).AsNoTracking();

			return query.FirstOrDefault();
		}
		
		public async Task<bool> CreateOrderProductAsync(Persistence.PostgreSql.Domain.OrderProduct request)
		{			
			using var dbContext = _dbContextFactory.CreateDbContext();

			await dbContext.OrderProduct.AddAsync(request);

			await dbContext.SaveChangesAsync();
				
			return true;
		}
		
		public async Task<bool> UpdateOrderProductAsync(Persistence.PostgreSql.Domain.OrderProduct request)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			dbContext.Entry(request).State = EntityState.Modified;
			
			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
