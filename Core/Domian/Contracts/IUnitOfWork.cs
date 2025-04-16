using Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChasgesAsync();

        // Generte Repository
        IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity:BaseEntity<TKey>;
    }
}
