using ConInfo.Models;
using FluentValidation;

namespace ConInfo.Validations
{
	public class EmployeeComIfoValidation : AbstractValidator<EmployeeComInfo>
	{
		public EmployeeComIfoValidation()
		{
			RuleFor(c => c.Id).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
			RuleFor(c => c.UnicInfo).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("Veri Alanı Boş Geçilemez");
			RuleFor(c => c.EmployeeId).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt Bulunamadı");
			RuleFor(c => c.ComType).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt Bulunamadı");
			RuleFor(c => c.Activity).GreaterThan(-1).LessThan(2).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt bulunamadı");
		}
	}
}
