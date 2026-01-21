namespace ECommence.Core.Models.Response
{
    public class CheckoutResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public int OrderId { get; set; }
    }
}
