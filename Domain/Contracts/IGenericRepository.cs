using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetAsync(TKey Id);
        Task<TEntity?> GetAsync(Specification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetAllAsync(bool IsTrackable=false);
        Task<IEnumerable<TEntity>> GetAllAsync( Specification<TEntity> specification);
        Task AddAsync(TEntity entity);
        void UpDate(TEntity entity);
        void Delete(TEntity entity);

        
    }
}
