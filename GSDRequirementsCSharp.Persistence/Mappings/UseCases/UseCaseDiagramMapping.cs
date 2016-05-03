using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCaseDiagramMapping : EntityTypeConfiguration<UseCaseDiagram>
    {
        public UseCaseDiagramMapping()
        {
            ToTable("UseCaseDiagram");
            HasKey(u => new { u.Id, u.Version });
        }
    }
}
