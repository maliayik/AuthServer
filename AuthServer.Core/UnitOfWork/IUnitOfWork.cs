using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.UnitOfWork
{
    /// <summary>
    /// İşlemleri tek bir transaction altında toplamak için kullanılır.
    /// </summary>
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
