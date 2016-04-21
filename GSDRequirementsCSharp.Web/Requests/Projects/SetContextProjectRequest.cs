using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDRequirementsCSharp.Web.Requests.Projects
{
    public class SetContextProjectRequest
    {
        private Guid _projectId;
        public string ProjectId
        {
            get { return _projectId.ToString(); }
            set { _projectId = Guid.Parse(value.Replace("string:", "")); }
        }

        public Guid GetProjectId()
        {
            return _projectId;
        }
    }
}