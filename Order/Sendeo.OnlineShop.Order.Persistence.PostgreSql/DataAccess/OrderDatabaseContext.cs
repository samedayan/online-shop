using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sendeo.OnlineShop.Order.Persistence.PostgreSql.Constants;

namespace Sendeo.OnlineShop.Order.Persistence.PostgreSql.DataAccess
{
	/// <summary>
	/// </summary>
	public class OrderDatabaseContext : DbContext
	{
		/// <summary>
		/// </summary>
		/// <param name="options"></param>
		public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options) : base(options)
		{
		}

		public DbSet<Domain.Order> Order { get; set; }

		/// <summary>
		/// </summary>
		/// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;

			optionsBuilder.UseNpgsql(string.Empty, builder => builder.EnableRetryOnFailure(3)).EnableSensitiveDataLogging();

			optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
		}

		/// <summary>
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(SchemaNames.CustomerDatabaseContextSchemaName);
			modelBuilder.Entity<Domain.Order>().ToTable("Order").HasKey(t => t.Id);
			modelBuilder.Entity<Domain.Order>().Property(t => t.Id).HasColumnName("Id");
			modelBuilder.Entity<Domain.Order>().Property(t => t.CustomerId).HasColumnName("CustomerId");
			modelBuilder.Entity<Domain.Order>().Property(t => t.ProductId).HasColumnName("ProductId");
			modelBuilder.Entity<Domain.Order>().Property(t => t.Quantity).HasColumnName("Quantity");
			modelBuilder.Entity<Domain.Order>().Property(t => t.Description).HasColumnName("Description");
			modelBuilder.Entity<Domain.Order>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate)
														.HasColumnName("CreatedDate");
			modelBuilder.Entity<Domain.Order>().OwnsOne(t => t.AuditInformation).Property(t => t.LastModifiedDate)
														.HasColumnName("LastModifiedDate");

		}
	}
}
