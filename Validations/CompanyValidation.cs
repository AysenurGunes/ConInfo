using ConInfo.Models;
using FluentValidation;

namespace ConInfo.Validations
{
	public class CompanyValidation:AbstractValidator<Company>
	{
		public CompanyValidation() 
		{
			RuleFor(c => c.Id).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
			RuleFor(c => c.Name).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim Alanı Boş Geçilemez");
			RuleFor(c => c.Name).MaximumLength(100).WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim Alanı En Fazla 100 karakter olmalıdır");
			RuleFor(c => c.Activity).GreaterThan(-1).LessThan(2).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
		}
	}
}
