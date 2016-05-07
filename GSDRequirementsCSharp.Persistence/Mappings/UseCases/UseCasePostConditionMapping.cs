﻿using GSDRequirementsCSharp.Domain.Models.UseCases;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings.UseCases
{
    class UseCasePostConditionMapping : EntityTypeConfiguration<UseCasePostCondition>
    {
        public UseCasePostConditionMapping()
        {
            HasKey(pc => pc.Id);
            Property(pc => pc.Id).HasColumnName("id");
            HasRequired(p => p.UseCase).WithMany(uc => uc.PostConditions);
        }
    }
}
