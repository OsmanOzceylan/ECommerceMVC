namespace ECommerceMVC.Core.Models.Request
{
    using System.ComponentModel.DataAnnotations;

    public class AdminLoginRequestModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; }
    }

    public class CustomerLoginRequestModel
    {
        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string CustomerPassword { get; set; }
    }
}
