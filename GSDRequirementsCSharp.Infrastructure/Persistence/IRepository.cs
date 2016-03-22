using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure
{
    public interface IRepository<TModel, TKey> where TModel : class, IEntity<TKey>
    {
        TModel Get(TKey id);
        void Add(TModel model);
        void Remove(TModel model);
        IEnumerable<TModel> GetAll();
    }
}
