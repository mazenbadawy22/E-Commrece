using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<IEnumerable<TEntity?>> GetAllAsync(bool TrackChanges=false)
        => TrackChanges ? await _storeContext.Set<TEntity>().ToListAsync()
            :await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity> GetByIdAsync(Tkey Id)
        => await _storeContext.Set<TEntity>().FindAsync(Id);
        public async Task AddAsync(TEntity entity)
        => await _storeContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
        => _storeContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)
        => _storeContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity?>> GetAllAsync(Specfications<TEntity> specfications)
        
         => await ApplySpceification(specfications).ToListAsync();


        public async Task<TEntity> GetByIdAsync(Specfications<TEntity> specfications)
       => await ApplySpceification(specfications).FirstOrDefaultAsync();
        private IQueryable<TEntity> ApplySpceification(Specfications<TEntity> specfications)
            => SpecificationEvaluator.GetQuery<TEntity>(_storeContext.Set<TEntity>(), specfications);

        public async Task<int> CountAsync(Specfications<TEntity> specfications)
       => await SpecificationEvaluator.GetQuery(_storeContext.Set<TEntity>(),specfications).CountAsync();
    }
}
