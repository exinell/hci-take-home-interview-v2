using PatientAdministrationSystem.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdministrationSystem.Application.Repositories.Interfaces
{
    public interface IEntitiesRepository<TEntity, TKey> where TEntity : Entity<TKey> where TKey : struct
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
