namespace ECommerceMVC.Core.Utilities
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public static Result<T> Ok(T data, string massage = "İşlem başarıyla tamamlandı.")
        {
            return new Result<T>
            {
                Success = true,
                Message = massage,
                Data = data
            };
        }

        public static Result<T> Fail(string message = "İşlem başarısız.")
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

    }
}
