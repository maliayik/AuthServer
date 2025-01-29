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