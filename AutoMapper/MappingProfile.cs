using AutoMapper;
using ConInfo.Dtos;
using ConInfo.Models;

namespace ConInfo.AutoMapper
{
	public class MappingProfile:Profile
	{
		public MappingProfile() 
		{
			CreateMap<PostCompanyDto, Company>();
			CreateMap<PostEmployeeDto, Employee>();
			CreateMap<PostEmployeeComInfoDto, EmployeeComInfo>();	
			CreateMap<PutCompanyDto, Company>();
			CreateMap<PutEmployeeDto, Employee>();
			CreateMap<PutEmployeeComInfoDto, EmployeeComInfo>();
		}
	}
}
