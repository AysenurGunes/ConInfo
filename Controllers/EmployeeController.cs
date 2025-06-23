using AutoMapper;
using ConInfo.DataAccess;
using ConInfo.Dtos;
using ConInfo.Models;
using ConInfo.Enums;
using ConInfo.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ConInfo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IConInfo<Employee> _employee;
		private readonly IMapper _mapper;
		private readonly ConInfoDbContext _dbContext;
		public EmployeeController(IConInfo<Employee> employee, IMapper mapper,ConInfoDbContext dbContext)
		{
			_employee = employee;
			_mapper = mapper;
			_dbContext= dbContext;
		}
		[HttpGet("GetAll")]
		public List<GetEmployeeDto> Get()
		{
			var query = from emp in _dbContext.Employees
						join comp in _dbContext.Companies on emp.CompanyId equals comp.Id
						where emp.Activity == 1
						select new GetEmployeeDto
						{
							Id = emp.Id,
							CompanyName = comp.Name,
							Name = emp.Name,
							CompanyId=comp.Id,
							UnicInfoValue = (from c in _dbContext.EmployeeComInfos where c.EmployeeId == emp.Id select new GetEmployeeComInfo{ ComTypeName=((EnumClass)c.ComType).ToString(),UnicInfo=c.UnicInfo }).ToList()
						};


			return query.ToList();
		}

		[HttpGet("GetByID")]
		public Employee Get([FromQuery] int id)
		{
			Expression<Func<Employee, bool>> expression = (c => c.Id == id && c.Activity == 1);
			return _employee.GetByID(expression);
		}
		
		[HttpGet("GetSearchByName")]
		public List<Employee> GetSearch([FromQuery] string Name)
		{
			Expression<Func<Employee, bool>> expression = (c => c.Name.Contains(Name) && c.Activity==1);
			return _employee.GetSpecial(expression).ToList();
		}

		[HttpGet("GetOrderByName")]
		public List<GetEmployeeDto> GetOrder()
		{
			List<GetEmployeeDto> employees = Get().OrderBy(c => c.Name).ToList();
			return employees;
		}

		[HttpPost]
		public ActionResult Post([FromBody] PostEmployeeDto employee)
		{
		

			var employee1 = _mapper.Map<Employee>(employee);
			employee1.Activity = 1;
			PostEmployeeValidation validations = new PostEmployeeValidation();
			validations.ValidateAndThrow(employee1);
			return StatusCode(_employee.Add(employee1));
		}


		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] PutEmployeeDto employee)
		{
			if (id != employee.Id)
			{
				return BadRequest();
			}

			var employee1 = _mapper.Map<Employee>(employee);
			employee1.Activity = 1;

			EmployeeValidation validations = new EmployeeValidation();
			validations.ValidateAndThrow(employee1);
			
			int result = _employee.Edit(employee1);
			return StatusCode(result);
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			Employee employee = Get(id);
			employee.Activity = 0;

			int result = _employee.Delete(employee);
			return StatusCode(result);
		}
	}
}
