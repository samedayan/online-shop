using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Constants;
using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Domain;

namespace Sendeo.OnlineShop.Customer.Persistence.PostgreSql.DataAccess
{
	/// <summary>
	/// </summary>
	public class CustomerDatabaseContext : DbContext
	{
		/// <summary>
		/// </summary>
		/// <param name="options"></param>
		public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options) : base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public DbSet<User> User { get; set; }

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
			modelBuilder.Entity<User>().ToTable("User").HasKey(t => t.Id);
			modelBuilder.Entity<User>().Property(t => t.Id).HasColumnName("Id");
			modelBuilder.Entity<User>().Property(t => t.Name).HasColumnName("Name");
			modelBuilder.Entity<User>().Property(t => t.LastName).HasColumnName("LastName");
			modelBuilder.Entity<User>().Property(t => t.Phone).HasColumnName("Phone");
			modelBuilder.Entity<User>().Property(t => t.Email).HasColumnName("Email");
			modelBuilder.Entity<User>().Property(t => t.Address).HasColumnName("Address");
			modelBuilder.Entity<User>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate)
														.HasColumnName("CreatedDate");
			modelBuilder.Entity<User>().OwnsOne(t => t.AuditInformation).Property(t => t.LastModifiedDate)
														.HasColumnName("LastModifiedDate");

		}
	}
}
