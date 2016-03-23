using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly GSDRequirementsContext _context;

        public GenericRepository(GSDRequirementsContext context)
        {
            _context = context;
        }

        public void Add(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
        }

        public TEntity Get(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity model)
        {
            _context.Set<TEntity>().Remove(model);
        }
    }
}
