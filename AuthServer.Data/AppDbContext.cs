using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data
{
    //identity api ile birlikte üyelik sistemi oluşucak bu üyelik işlemlerini aynı dbde tutmak istiyorum eğer farklı bir dbde tutmak istiyorsak farklı dbcontext sınıfı oluşturmamız gerekir.
    public class AppDbContext:IdentityDbContext<UserApp,IdentityRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //identity ile ilgili db setler otomatik olarak gelicek burada sadece kendi tablolarımızı ekliyoruz.
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefleshToken> UserRefleshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
