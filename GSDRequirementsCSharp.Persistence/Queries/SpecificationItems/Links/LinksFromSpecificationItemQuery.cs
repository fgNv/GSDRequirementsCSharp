using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.SpecificationItems.Links
{
    public class LinksFromSpecificationItemQuery
    {
        public Guid Id { get; set; }

        public static implicit operator LinksFromSpecificationItemQuery(Guid id)
        {
            return new LinksFromSpecificationItemQuery { Id = id };
        }
    }
}
