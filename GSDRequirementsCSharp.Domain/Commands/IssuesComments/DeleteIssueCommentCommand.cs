﻿using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands.IssuesComments
{
    public class DeleteIssueCommentCommand : ICommand
    {
        public Guid IssueCommentId { get; set; }

        public static implicit operator DeleteIssueCommentCommand(Guid id)
        {
            return new DeleteIssueCommentCommand { IssueCommentId = id };
        }
    }
}