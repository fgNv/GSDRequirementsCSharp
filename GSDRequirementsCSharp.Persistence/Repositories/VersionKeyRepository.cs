using GSDRequirementsCSharp.Domain;
using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    public class VersionKeyRepository<T> : IRepository<T, VersionKey>
        where T : class, IEntity<VersionKey>
    {
        private readonly GSDRequirementsContext _context;

        public VersionKeyRepository(GSDRequirementsContext context)
        {
            _context = context;
        }

        public void Add(T model)
        {
            _context.Set<T>().Add(model);
        }

        public T Get(VersionKey id)
        {
            return _context.Set<T>().Find(id.Id, id.Version);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(T model)
        {
            _context.Set<T>().Remove(model);
        }
    }
}
