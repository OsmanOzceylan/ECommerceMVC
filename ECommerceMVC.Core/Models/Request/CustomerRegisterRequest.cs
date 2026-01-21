using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Core.Models.Request
{
    public class CustomerRegisterRequest
    {
        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Müşteri soyadı zorunludur.")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string CustomerPassword { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
    }
}
