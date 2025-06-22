using ConInfo.Models;

namespace ConInfo.DataAccess
{
	public class DataGenerator
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var dbcontext = new ConInfoDbContext())
			{
				if (dbcontext.Companies.Any())
				{
					return;
				}
				if (dbcontext.Employees.Any())
				{
					return;
				}

				dbcontext.Companies.AddRange(new Company
				{

					Name = "ACom"
				},
				new Company
				{

					Name = "BCom"
				});

				dbcontext.SaveChanges();
				dbcontext.Employees.AddRange(new Employee
				{
					CompanyId = dbcontext.Companies.First().Id,
					Name = "Bugs Bunny",

				});

				dbcontext.SaveChanges();
				dbcontext.EmployeeComInfos.AddRange(
					new EmployeeComInfo
					{
						ComType = 1,
						EmployeeId = dbcontext.Employees.First().Id,
						UnicInfo = "bugs@mail"

					},
					new EmployeeComInfo
					{
						ComType = 2,
						EmployeeId = dbcontext.Employees.First().Id,
						UnicInfo = "154978"
					}
					);


				dbcontext.SaveChanges();
			}
		}
	}
}
