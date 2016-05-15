using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Queries
{
    public class AuditingsByPeriodQuery
    {
        private AuditingPeriod? _period;
        public AuditingPeriod Period
        {
            get
            {
                if (_period.HasValue)
                    return _period.Value;
                return AuditingPeriod.LastWeek;
            }
            set { _period = value; }
        }
    }
}
