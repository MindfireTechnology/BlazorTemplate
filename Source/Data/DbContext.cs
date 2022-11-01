using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : DbContext
{
	public DbSet<UserEntity> Users { get; set; }
	public DbSet<AddressEntity> Addresses { get; set; }

	public DatabaseContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<AddressEntity>()
			.HasOne(a => a.User)
			.WithMany(u => u.Addresses)
			.HasForeignKey(x => x.UserId);
	}
}
