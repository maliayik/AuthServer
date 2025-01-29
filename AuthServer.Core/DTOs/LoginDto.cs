namespace AuthServer.Core.DTOs
{
    /// <summary>
    /// Kullanıcı giriş işlemleri sonrası login durumuna göre token üretmek için kullanılacak model
    /// </summary>
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}