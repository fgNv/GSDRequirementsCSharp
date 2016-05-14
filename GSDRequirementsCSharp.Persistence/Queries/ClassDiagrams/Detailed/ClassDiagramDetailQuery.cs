using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Queries.ClassDiagrams.Detailed
{
    public class ClassDiagramDetailQuery
    {
        public Guid Id { get; set; }

        public int? Version { get; set; }

        public static implicit operator ClassDiagramDetailQuery(Guid id)
        {
            return new ClassDiagramDetailQuery { Id = id };
        }
    }
}
