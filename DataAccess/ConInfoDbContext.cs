using Microsoft.EntityFrameworkCore;
using ConInfo.Models;
namespace ConInfo.DataAccess
{
	public class ConInfoDbContext : DbContext
	{
		private readonly IConfiguration _configuration;
		public ConInfoDbContext()
		{

		}
		public ConInfoDbContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string con1 = @"Server=DESKTOP-TN9J4B3\SQLEXPRESS; Database=ConfInfoDb;User Id=Guest; Password=123pass; TrustServerCertificate=True";
			string con2 = @"Server=localhost\SQLEXPRESS; Database=ConInfoDb;User Id=sa; Password=123456; TrustServerCertificate=True";
			optionsBuilder.UseSqlServer(con2);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>().HasKey(x => x.Id).IsClustered();
			modelBuilder.Entity<Company>().Property(x => x.Name);
			modelBuilder.Entity<Company>().Property(x => x.Activity).HasDefaultValue(1);


			modelBuilder.Entity<Employee>().HasKey(x => x.Id).IsClustered();
			modelBuilder.Entity<Employee>().Property(x => x.Name);
			modelBuilder.Entity<Employee>().Property(x => x.CompanyId);
			modelBuilder.Entity<Employee>().Property(x => x.Activity).HasDefaultValue(1);

			modelBuilder.Entity<EmployeeComInfo>().HasKey(x => x.Id).IsClustered();
			modelBuilder.Entity<EmployeeComInfo>().Property(x => x.EmployeeId);
			modelBuilder.Entity<EmployeeComInfo>().Property(x => x.ComType);
			modelBuilder.Entity<EmployeeComInfo>().Property(x => x.UnicInfo);
			modelBuilder.Entity<EmployeeComInfo>().Property(x => x.Activity).HasDefaultValue(1);


		}
		public DbSet<Company> Companies { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<EmployeeComInfo> EmployeeComInfos { get; set; }

	}
}
