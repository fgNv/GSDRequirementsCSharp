using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries.ClassDiagrams
{
    public class ClassDiagramNextIdQuery
    {
        public Guid ProjectId { get; set; }

        public static implicit operator ClassDiagramNextIdQuery(Guid id)
        {
            return new ClassDiagramNextIdQuery { ProjectId = id };
        }
    }
}
