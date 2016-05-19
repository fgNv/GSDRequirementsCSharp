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

        public static Att Placeholder(string value)
        {
            return new Att("placeholder", value);
        }

        public static Att NgMask(string value)
        {
            return new Att("data-mask", value);
        }

        public static Att NgMaskNumber()
        {
            return NgMask("9999999999999");
        }

        public static Att NgMaskPhone()
        {
            return NgMask("+99 999 99999 9999 9999");
        }

        public static Att Length(int maxLength)
        {
            return new Att("length", maxLength.ToString());
        }
    }
}