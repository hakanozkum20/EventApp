using EventApp.Application.ViewModels.Companies;
using FluentValidation;

namespace EventApp.Application.Validators.Companies
{
    public class VM_Create_CompanyValidator : AbstractValidator<VM_Create_Company>
    {
        public VM_Create_CompanyValidator()
        {
            // Name alanı için kurallar
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şirket adı boş olamaz.")
                .MinimumLength(2).WithMessage("Şirket adı en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Şirket adı en fazla 100 karakter olabilir.");

            // CustomerId alanı için kurallar (opsiyonel olduğu için sadece değer varsa kontrol ediyoruz)
            RuleFor(x => x.CustomerId)
                .Must(id => id == null || id != Guid.Empty).WithMessage("Geçersiz müşteri ID'si.");
        }
    }
}
