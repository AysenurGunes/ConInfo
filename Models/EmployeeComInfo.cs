namespace ConInfo.Models
{
	public class EmployeeComInfo
	{
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }
		public int ComType { get; set; }
		public string UnicInfo { get; set; }
		public int Activity { get; set; }

	}
}
