using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Core.Models.Request
{
    public class CustomerLoginRequestModel
    {
        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string CustomerPassword { get; set; }
    }
}
