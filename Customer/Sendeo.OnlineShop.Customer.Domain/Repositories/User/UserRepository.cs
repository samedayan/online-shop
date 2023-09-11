using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;
using Sendeo.OnlineShop.Customer.Domain.Extensions;
using Sendeo.OnlineShop.Customer.Infrastructure.Exceptions;
using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.DataAccess;
using System.Linq.Expressions;

namespace Sendeo.OnlineShop.Customer.Domain.Repositories.User
{
	public class UserRepository : IUserRepository
	{
		private readonly IDbContextFactory<CustomerDatabaseContext> _dbContextFactory;

		public UserRepository(IDbContextFactory<CustomerDatabaseContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
		}

		public (int totalCount, IReadOnlyCollection<Persistence.PostgreSql.Domain.User>) FindUsers(FindUserQuery request)
		{
			Expression<Func<Persistence.PostgreSql.Domain.User, bool>> predicate = x => true;

			if (!string.IsNullOrEmpty(request.Email))
				predicate = predicate.And(s => request.Email.Contains(s.Email));

			if (!string.IsNullOrEmpty(request.Phone))
				predicate = predicate.And(s => request.Phone.Contains(s.Phone));

			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.User				
				.Where(predicate)
				.OrderByDescending(s => s.Id)
				.AsNoTracking();

			var count = query.Count();

			if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

			if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);

			return (count, query.ToList());
		}

		public Persistence.PostgreSql.Domain.User? FindUserById(FindUserByIdQuery request)
		{
			Expression<Func<Persistence.PostgreSql.Domain.User, bool>> predicate = x => true;

			predicate = predicate.And(s => s.Id == request.Id);

			using var dbContext = _dbContextFactory.CreateDbContext();

			var query = dbContext.User.Where(predicate).AsNoTracking();

			return query.FirstOrDefault();
		}

		public async Task<bool> CreateUserAsync(Persistence.PostgreSql.Domain.User request)
		{			
			using var dbContext = _dbContextFactory.CreateDbContext();

			if (!string.IsNullOrEmpty(request.Email) && dbContext.User.Any(s => s.Email == request.Email))
			{
				throw new BusinessException("Registered Email Address!", ExceptionCodes.DefaultExceptionCode);
			}

			if (!string.IsNullOrEmpty(request.Phone) && dbContext.User.Any(s => s.Phone == request.Phone))
			{
				throw new BusinessException("Registered Phone Number!", ExceptionCodes.DefaultExceptionCode);
			}

			await dbContext.User.AddAsync(request);

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> UpdateUserAsync(Persistence.PostgreSql.Domain.User request)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			var model = await dbContext.User.FirstOrDefaultAsync(s => s.Id == request.Id);

			if (model is null)
			{
				throw new BusinessException("User Not Found!", ExceptionCodes.DefaultExceptionCode);
			}

			model.Address = request.Address;
			model.Email = request.Email;
			model.Phone = request.Phone;
			model.Name = request.Name;
			model.LastName = request.LastName;
			model.AuditInformation.CreatedDate = request.AuditInformation.CreatedDate;
			model.AuditInformation.LastModifiedDate = DateTime.Now.ToUniversalTime();

			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteUserAsync(DeleteUserCommand request)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();

			var user = dbContext.User.FirstOrDefault(s => s.Id == request.Id);

			if (user is null)
			{
				throw new BusinessException("User Not Found!", ExceptionCodes.DefaultExceptionCode);
			}

			dbContext.User.Remove(user);

			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
