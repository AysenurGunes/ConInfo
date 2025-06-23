using AutoMapper;
using ConInfo.DataAccess;
using ConInfo.Dtos;
using ConInfo.Enums;
using ConInfo.Models;
using ConInfo.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConInfo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeComInfoController : ControllerBase
	{
		/// <summary>
		/// Personellerin birden fazla iletişim bilgisi için 
		/// </summary>
		private readonly IConInfo<EmployeeComInfo> _employeeComInfo;
		private readonly IMapper _mapper;
		private readonly ConInfoDbContext _dbContext;
		public EmployeeComInfoController(IConInfo<EmployeeComInfo> employeeComInfo, IMapper mapper, ConInfoDbContext dbContext)
		{
			_employeeComInfo = employeeComInfo;
			_mapper = mapper;
			_dbContext = dbContext;
		}
		[HttpGet("GetAll")]
		public List<EmployeeComInfo> Get()
		{
			return _employeeComInfo.GetAll().ToList();
		}

		[HttpGet("GetByID")]
		public EmployeeComInfo Get([FromQuery] int id)
		{
			Expression<Func<EmployeeComInfo, bool>> expression = (c => c.Id == id && c.Activity == 1);
			return _employeeComInfo.GetByID(expression);
		}
		/// <summary>
		/// employee göre iletişim bilgileri
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("GetByEmployeeID")]
		public List<GetEmployeeComInfo> GetByEmployee(int id)
		{
			var query = from empCom in _dbContext.EmployeeComInfos
						join emp in _dbContext.Employees on empCom.EmployeeId equals emp.Id
						where emp.Activity == 1 && empCom.Activity==1 && empCom.EmployeeId==id
						select new GetEmployeeComInfo { ComTypeName = ((EnumClass)empCom.ComType).ToString(), UnicInfo = empCom.UnicInfo };


			return query.ToList();
		}

	

		[HttpPost]
		public ActionResult Post([FromBody] PostEmployeeComInfoDto employeeComInfo)
		{
			

			var employeeComInfo1 = _mapper.Map<EmployeeComInfo>(employeeComInfo);
			employeeComInfo1.Activity = 1;

			PostEmployeeComInfoValidation validations = new PostEmployeeComInfoValidation();
			validations.ValidateAndThrow(employeeComInfo1);

			return StatusCode(_employeeComInfo.Add(employeeComInfo1));
		}


		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] PutEmployeeComInfoDto employeeComInfo)
		{
			if (id != employeeComInfo.Id)
			{
				return BadRequest();
			}

			var employeeComInfo1 = _mapper.Map<EmployeeComInfo>(employeeComInfo);
			employeeComInfo1.Activity = 1;

			EmployeeComIfoValidation validations = new EmployeeComIfoValidation();
			validations.ValidateAndThrow(employeeComInfo1);

			int result = _employeeComInfo.Edit(employeeComInfo1);
			return StatusCode(result);
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			EmployeeComInfo employeeComInfo = Get(id);
			employeeComInfo.Activity = 0;

			int result = _employeeComInfo.Delete(employeeComInfo);
			return StatusCode(result);
		}
	}
}
