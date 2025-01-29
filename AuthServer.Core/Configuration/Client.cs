namespace AuthServer.Core.Configuration
{
    /// <summary>
    /// Authorization server'a istek yapacak clientlara karşılık gelen sınıf
    /// </summary>
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        /// <summary>
        /// Hangi api'ye erişim izni var bilgisini tutar
        /// </summary>
        public List<String> Audiences { get; set; }
    }
}