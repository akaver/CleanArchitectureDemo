using System;
using System.Threading.Tasks;

namespace BLL.Core
{
    public interface IBLL
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}