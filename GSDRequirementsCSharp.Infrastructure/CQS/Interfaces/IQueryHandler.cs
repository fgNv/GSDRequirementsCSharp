﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.CQS
{
    public interface IQueryHandler<TQuery, TResult>
    {
        TResult Handle(TQuery query);
    }
}
