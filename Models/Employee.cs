namespace ConInfo.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public int Activity { get; set; }

	}
}
