using AutoMapper;
using ConInfo.DataAccess;
using ConInfo.Dtos;
using ConInfo.Models;
using ConInfo.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ConInfo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly IConInfo<Company> _company;
		private readonly IConInfo<Employee> _employee;
		private readonly IMapper _mapper;
		public CompanyController(IConInfo<Company> company, IMapper mapper, IConInfo<Employee> employee)
		{
			_company = company;
			_employee = employee;
			_mapper = mapper;
		}
		[HttpGet("GetAllCompany")]
		public List<Company> Get()
		{
			return _company.GetAll().Where(c => c.Activity == 1).ToList();
		}
		/// <summary>
		/// base repostory ile id ye göre data çeker
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("GetByID")]
		public Company Get([FromQuery] int id)
		{
			Expression<Func<Company, bool>> expression = (c => c.Id == id && c.Activity == 1);
			return _company.GetByID(expression);
		}

		[HttpGet("GetSearchByName")]
		public List<Company> GetSearch([FromQuery] string Name)
		{
			Expression<Func<Company, bool>> expression = (c => c.Name.Contains(Name));
			return _company.GetSpecial(expression).ToList();
		}

		[HttpGet("GetOrderByName")]
		public List<Company> GetOrder()
		{
			List<Company> companies = Get().OrderBy(c => c.Name).ToList();
			return companies;
		}

		[HttpPost]
		public ActionResult Post([FromBody] PostCompanyDto company)
		{


			var company1 = _mapper.Map<Company>(company);
			company1.Activity = 1;
			PostCompanyValidation validations = new PostCompanyValidation();
			validations.ValidateAndThrow(company1);
			return StatusCode(_company.Add(company1));
		}
		/// <summary>
		/// validation kontrolu sonucu güncelleme yapılır
		/// </summary>
		/// <param name="id"></param>
		/// <param name="company"></param>
		/// <returns></returns>

		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] PutCompanyDto company)
		{
			if (id != company.Id)
			{
				return BadRequest();
			}

			var company1 = _mapper.Map<Company>(company);
			company1.Activity = 1;

			CompanyValidation validations = new CompanyValidation();
			validations.ValidateAndThrow(company1);

			int result = _company.Edit(company1);
			return StatusCode(result);
		}
		/// <summary>
		/// Company silme servisidir baplı employee varsa silmez
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			Expression<Func<Employee, bool>> expression = (c => c.CompanyId == id && c.Activity == 1);
			var resultEmp= _employee.GetByID(expression);
			if (resultEmp==null)
			{
				Company company = Get(id);
				company.Activity = 0;

				int result = _company.Delete(company);
				return StatusCode(result);
			}
			else
			{
				return BadRequest("Bağlı Personel");
			}
			
		}
	}
}
