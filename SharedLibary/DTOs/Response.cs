using System.Text.Json.Serialization;

namespace SharedLibary.DTOs
{
    /// <summary>
    /// Tüm API'lerde dönülecek cevapların tutulduğu ortak sınıf
    /// </summary>
    public class Response<T> where T : class
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }
        public ErrorDto Error { get; private set; }

        /// <summary>
        /// clientlara gösterilmeyecek,uygulama içerisinde kontrol amaçlı kullanılacak property
        /// </summary>
        [JsonIgnore]
        public bool isSuccessful { get; private set; }

        /// <summary>
        /// Data ve StatusCode döneceği zaman kullanılır
        /// </summary>
        public static Response<T> Success(T data, int statusCode) => new Response<T> { Data = data, StatusCode = statusCode, isSuccessful = true };

        /// <summary>
        /// Data boş döneceği zaman kullanılır (örneğin silme,update işlemlerinde)
        /// </summary>
        public static Response<T> Success(int statusCode) => new Response<T> { Data = default, StatusCode = statusCode, isSuccessful = true };

        /// <summary>
        /// Çoklu Hata durumlarında kullanılır
        /// </summary>
        public static Response<T> Fail(ErrorDto errorDto, int statusCode) => new Response<T> { Error = errorDto, StatusCode = statusCode, isSuccessful = false };

        /// <summary>
        /// Tek bir hata döneceği zaman kullanılır
        /// </summary>
        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new Response<T>()
            {
                Error = errorDto,
                StatusCode = statusCode,
                isSuccessful = false
            };
        }
    }
}