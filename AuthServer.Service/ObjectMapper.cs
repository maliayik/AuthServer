using AutoMapper;

namespace AuthServer.Service
{
    public static class ObjectMapper
    {
        //uygulama ilk çalıştığında biz kullanana kadar bellekte yer kaplamaz bu yüzden lazy loading kullanıyoruz.
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}