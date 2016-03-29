using GSDRequirementsCSharp.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GSDRequirementsCSharp.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            filters.Add(new CurrentCultureSetterMvcFilter());
            filters.Add(new HandleMvcException());
        }
    }
}