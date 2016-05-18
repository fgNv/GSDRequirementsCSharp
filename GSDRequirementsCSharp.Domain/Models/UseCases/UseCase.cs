namespace GSDRequirementsCSharp.Domain
{
    using Models;
    using System;
    using System.Collections.Generic;

    public class UseCase : UseCaseEntity
    {
        public const string PREFIX = "UC";
        public override Guid Id { get; set; }
        public override UseCaseEntityType Type
        {
            get { return UseCaseEntityType.UseCase; }
        }
        public ICollection<UseCaseContent> Contents { get; set; }
        
        public int Identifier { get; set; }

        public ICollection<UseCasePreCondition> PreConditions { get; set; }

        public ICollection<UseCasePostCondition> PostConditions { get; set; }

        public SpecificationItem SpecificationItem { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }
        
        public string IsLastVersionChar { get; set; }
        public bool IsLastVersion
        {
            get
            {
                return IsLastVersionChar == "True";
            }
            set
            {
                IsLastVersionChar = value ? "True" : "False";
            }
        }

        public UseCase()
        {
            Contents = new HashSet<UseCaseContent>();
            PreConditions = new HashSet<UseCasePreCondition>();
            PostConditions = new HashSet<UseCasePostCondition>();
        }
    }
}
