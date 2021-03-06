﻿using GSDRequirementsCSharp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Persistence.Mappings
{
    public class IssueCommentMapping : EntityTypeConfiguration<IssueComment>
    {
        public IssueCommentMapping()
        {
            ToTable("IssueComment");

            Property(e => e.Id).HasColumnName("id");
            Property(e => e.CreatorId).HasColumnName("creator_id");
            Property(e => e.IssueId).HasColumnName("issue_id");
            Property(e => e.CreatedAt).HasColumnName("created_at");
            HasMany(e => e.Contents).WithRequired(e => e.IssueComment);
            Property(e => e.LastModification).HasColumnName("last_modification");
            Property(e => e.Active).HasColumnName("active");
        }
    }
}
