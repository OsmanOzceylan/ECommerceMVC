using FluentValidation;

namespace ECommerceMVC.Entities.Models
{
    public class OrderInfoValidator : AbstractValidator<OrderInfo>
    {
        public OrderInfoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email boş bırakılamaz.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş bırakılamaz.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş bırakılamaz.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Posta kodu boş bırakılamaz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");
            RuleFor(x => x.CardNumber).NotEmpty().CreditCard().WithMessage("Kart numarası boş bırakılamaz.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Kart Üzerindeki isim boş bırakılamaz.");
            RuleFor(x => x.CVV).NotEmpty().Matches(@"^\d{3,4}$").WithMessage("CVV boş bırakılamaz.");
        }
        
    }
}
