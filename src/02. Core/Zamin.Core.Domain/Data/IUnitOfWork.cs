using System.Threading.Tasks;

namespace Zamin.Core.Domain.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        int Commit();
        Task<int> CommitAsync();
    }
}