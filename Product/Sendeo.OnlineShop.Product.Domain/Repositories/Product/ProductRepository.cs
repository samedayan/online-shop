using Microsoft.EntityFrameworkCore;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Domain.Extensions;
using Sendeo.OnlineShop.Product.Persistence.PostgreSql.DataAccess;
using System.Linq.Expressions;

namespace Sendeo.OnlineShop.Product.Domain.Repositories.Product
{
	public class ProductRepository : IProductRepository
	{
		private readonly IDbContextFactory<ProductDatabaseContext> _dbContextFactory;

		public ProductRepository(IDbContextFactory<ProductDatabaseContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
		}

		public (int totalCount, IReadOnlyCollection<Persistence.PostgreSql.Domain.Product>) GetProducts(GetProductQuery request)
		{
			Expression<Func<Persistence.PostgreSql.Domain.Product, bool>> predicate = x => true;

			// TODO Category Enum

			if (!string.IsNullOrEmpty(request.Name))
				predicate = predicate.And(s => request.Name.Contains(s.Name));

			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.Product				
				.Where(predicate)
				.OrderByDescending(s => s.Id)
				.AsNoTracking();

			var count = query.Count();

			if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

			if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);

			return (count, query.ToList());
		}

		public Persistence.PostgreSql.Domain.Product? GetProductById(GetProductByIdQuery request)
		{
			Expression<Func<Persistence.PostgreSql.Domain.Product, bool>> predicate = x => true;

			predicate = predicate.And(s => s.Id == request.Id);

			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.Product.Where(predicate).AsNoTracking();

			return query.FirstOrDefault();
		}

		public async Task<bool> CreateProductAsync(Persistence.PostgreSql.Domain.Product product)
		{			
			using var dbContext = _dbContextFactory.CreateDbContext();

			// TODO Validation Up Level

			await dbContext.Product.AddAsync(product);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> UpdateProductAsync(Persistence.PostgreSql.Domain.Product product)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteProductAsync(Persistence.PostgreSql.Domain.Product product)
		{
			// TODO: Validation up level
			using var dbContext = _dbContextFactory.CreateDbContext();

			dbContext.Product.Remove(product);

			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
