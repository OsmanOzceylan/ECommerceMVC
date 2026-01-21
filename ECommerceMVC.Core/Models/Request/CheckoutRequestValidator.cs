using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Core.Models.Request
{
    public class CheckoutRequestValidator : AbstractValidator<CheckoutRequest>
    {
        public CheckoutRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş bırakılamaz.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş bırakılamaz.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Posta kodu boş bırakılamaz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");
            RuleFor(x => x.CardNumber)
    .NotEmpty().WithMessage("Kart numarası boş bırakılamaz.")
    .Must(x => x.Replace(" ", "").Replace("-", "").Length >= 12)
    .WithMessage("Lütfen geçerli bir kredi kartı numarası girin.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Kart üzerindeki isim boş bırakılamaz.");
            RuleFor(x => x.CVV).NotEmpty().Matches(@"^\d{3,4}$").WithMessage("CVV boş bırakılamaz.");
        }
    }
}
