using ConInfo.Models;

namespace ConInfo.Dtos
{
	public class GetEmployeeDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CompanyName { get; set; }
		public int CompanyId { get; set; }
		public List<GetEmployeeComInfo> UnicInfoValue { get; set; }
	}
	
}
