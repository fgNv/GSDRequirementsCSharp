﻿using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    class ClassDiagramMapping : EntityTypeConfiguration<ClassDiagram>
    {
        public ClassDiagramMapping()
        {
            ToTable("ClassDiagram");
            HasKey(cd => new { cd.Id, cd.Locale });
            Property(cd => cd.Id).HasColumnName("id");
            Property(cd => cd.Name).HasColumnName("name");
            Property(cd => cd.Locale).HasColumnName("locale");

            Property(e => e.Name)
                            .IsUnicode(false);
            Property(e => e.Locale)
                            .IsUnicode(false);
        }
    }
}
