using AutoMapper;
using ConInfo.DataAccess;
using ConInfo.Dtos;
using ConInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ConInfo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly IConInfo<Company> _company;
		private readonly IMapper _mapper;
		public CompanyController(IConInfo<Company> company, IMapper mapper)
		{
			_company = company;
			_mapper = mapper;
		}
		[HttpGet("GetAllCompany")]
		public List<Company> Get()
		{
			return _company.GetAll().Where(c=>c.Activity==1).ToList();
		}

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
			//PostBookValidation validations = new PostBookValidation();
			//validations.ValidateAndThrow(book);

			var company1 = _mapper.Map<Company>(company);
			return StatusCode(_company.Add(company1));
		}


		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] PutCompanyDto company)
		{
			if (id != company.Id)
			{
				return BadRequest();
			}

			var company1 = _mapper.Map<Company>(company);

			//BookValidation validations = new BookValidation();
			//validations.ValidateAndThrow(book1);
			company1.Activity = 1;
			int result = _company.Edit(company1);
			return StatusCode(result);
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			Company company = Get(id);
			company.Activity = 0;

			int result = _company.Delete(company);
			return StatusCode(result);
		}
	}
}
