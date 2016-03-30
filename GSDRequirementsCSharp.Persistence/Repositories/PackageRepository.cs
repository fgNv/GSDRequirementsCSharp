using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Repositories
{
    class PackageRepository : IRepository<Package, PackageKey>
    {
        private readonly GSDRequirementsContext _context;

        public PackageRepository(GSDRequirementsContext context)
        {
            _context = context;
        }

        public void Add(Package model)
        {
            _context.Packages.Add(model);
        }

        public Package Get(PackageKey id)
        {
            return _context.Packages.Find(id.Id, id.Locale);
        }
        
        public IEnumerable<Package> GetAll()
        {
            return _context.Packages.ToList();
        }

        public void Remove(Package model)
        {
            _context.Packages.Remove(model);
        }
    }
}
