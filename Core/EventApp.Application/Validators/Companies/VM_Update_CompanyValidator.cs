using EventApp.Application.ViewModels.Companies;
using FluentValidation;

namespace EventApp.Application.Validators.Companies
{
    public class VM_Update_CompanyValidator : AbstractValidator<VM_Update_Company>
    {
        public VM_Update_CompanyValidator()
        {
            // ID alanı için kurallar
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Şirket ID'si boş olamaz.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Geçersiz ID formatı.");

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
