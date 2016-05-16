using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Validation.Attributes
{
    public class AtLeastOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
                return list.Count > 0;

            var enumerable = value as IEnumerable;
            foreach (var item in enumerable)
                return true; //if at least one item, return true

            return false;
        }
    }
}
