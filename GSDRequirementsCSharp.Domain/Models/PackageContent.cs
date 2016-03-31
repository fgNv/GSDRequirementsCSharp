using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Models
{
    public class PackageContent : IEntity<LocaleKey>
    {
        public Guid Id { get; set; }
        public Package Package { get; set; }
        public string Description { get; set; }        
        public string Locale { get; set; }

        LocaleKey IEntity<LocaleKey>.Id
        {
            get
            {
                return new LocaleKey {  Id = Id, Locale = Locale};
            }
        }
    }
}
