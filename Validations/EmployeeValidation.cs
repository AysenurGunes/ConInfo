
using ConInfo.Models;
using FluentValidation;

namespace ConInfo.Validations
{
	public class EmployeeValidation : AbstractValidator<Employee>
	{
		public EmployeeValidation()
		{
			RuleFor(c => c.Id).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
			RuleFor(c => c.Name).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim Alanı Boş Geçilemez");
			RuleFor(c => c.Name).MaximumLength(50).WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim Alanı En Fazla 50 karakter olmalıdır");
			RuleFor(c => c.CompanyId).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt Bulunamadı");
			RuleFor(c => c.Activity).GreaterThan(-1).LessThan(2).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
		}
	}
}
