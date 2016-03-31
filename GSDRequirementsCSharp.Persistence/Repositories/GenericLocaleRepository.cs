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
    public class GenericLocaleRepository<T> : IRepository<T, LocaleKey>
        where T : class, IEntity<LocaleKey>
    {
        private readonly GSDRequirementsContext _context;

        public GenericLocaleRepository(GSDRequirementsContext context)
        {
            _context = context;
        }

        public void Add(T model)
        {
            _context.Set<T>().Add(model);
        }

        public T Get(LocaleKey id)
        {
            return _context.Set<T>().Find(id.Id, id.Locale);
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
