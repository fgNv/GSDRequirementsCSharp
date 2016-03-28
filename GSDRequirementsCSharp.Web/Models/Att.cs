using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Models
{
    public class Att
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Att(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static Att NgModel(string value)
        {
            return new Att("data-ng-model", value);
        }
    }
}