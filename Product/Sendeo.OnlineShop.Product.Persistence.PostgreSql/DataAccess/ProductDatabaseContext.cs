using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sendeo.OnlineShop.Product.Persistence.PostgreSql.Constants;

namespace Sendeo.OnlineShop.Product.Persistence.PostgreSql.DataAccess
{
	/// <summary>
	/// </summary>
	public class ProductDatabaseContext : DbContext
	{
		/// <summary>
		/// </summary>
		/// <param name="options"></param>
		public ProductDatabaseContext(DbContextOptions<ProductDatabaseContext> options) : base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public DbSet<Domain.Product> Product { get; set; }

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
			modelBuilder.HasDefaultSchema(SchemaNames.ProductDatabaseContextSchemaName);
			modelBuilder.Entity<Domain.Product>().ToTable("Product").HasKey(t => t.Id);
			modelBuilder.Entity<Domain.Product>().Property(t => t.Id).HasColumnName("Id");
			modelBuilder.Entity<Domain.Product>().Property(t => t.Code).HasColumnName("Code");
			modelBuilder.Entity<Domain.Product>().Property(t => t.Name).HasColumnName("Name");
			modelBuilder.Entity<Domain.Product>().Property(t => t.CategoryId).HasColumnName("CategoryId");
			modelBuilder.Entity<Domain.Product>().Property(t => t.Description).HasColumnName("Description");
			modelBuilder.Entity<Domain.Product>().Property(t => t.ImagePath).HasColumnName("ImagePath");
			modelBuilder.Entity<Domain.Product>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate)
														.HasColumnName("CreatedDate");
			modelBuilder.Entity<Domain.Product>().OwnsOne(t => t.AuditInformation).Property(t => t.LastModifiedDate)
														.HasColumnName("LastModifiedDate");

		}
	}
}
