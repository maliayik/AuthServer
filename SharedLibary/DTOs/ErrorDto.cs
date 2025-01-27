using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibary.DTOs
{
    //Tüm API'lerde dönülecek hataların tutulduğu ortak sınıf
    public class ErrorDto
    {
        //başka bir sınıftan set edilmesini engellemek için private set kullanıldı
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; }//client tarafında hata mesajlarını göstermek isteyip istemediğini belirleyen property

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        //tek hata durumları için
        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        //birden fazla hata durumları için
        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }

    }
}
