using ConInfo.Models;
using FluentValidation;

namespace ConInfo.Validations
{
	public class PostCompanyValidation : AbstractValidator<Company>
	{
		public PostCompanyValidation()
		{
			RuleFor(c => c.Name).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim Alanı Boş Geçilemez");
			RuleFor(c => c.Name).MaximumLength(100).WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim Alanı En Fazla 100 karakter olmalıdır");
			RuleFor(c => c.Activity).GreaterThan(-1).LessThan(2).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
		}
	}
}
