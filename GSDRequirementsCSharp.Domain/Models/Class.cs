namespace GSDRequirementsCSharp.Domain
{
    using System;
    using System.Collections.Generic;
    
    public class Class
    {
        public Class()
        {
            ClassContents = new HashSet<ClassContent>();
            ClassMethods = new HashSet<ClassMethod>();
            ClassProperties = new HashSet<ClassProperty>();
        }

        public Guid Id { get; set; }

        public int Visibility { get; set; }

        public Guid ClassDiagramId { get; set; }
        
        public virtual ICollection<ClassContent> ClassContents { get; set; }
        
        public virtual ICollection<ClassMethod> ClassMethods { get; set; }
        
        public virtual ICollection<ClassProperty> ClassProperties { get; set; }
    }
}
