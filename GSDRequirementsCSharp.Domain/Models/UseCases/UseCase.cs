namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Models;
    using Models.UseCases;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UseCase : UseCaseEntity
    {
        public const string PREFIX = "UC";
        public override Guid Id { get; set; }
        public ICollection<UseCaseContent> Contents { get; set; }
        public override UseCaseEntityType Type { get { return UseCaseEntityType.UseCase; } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Identifier { get; set; }
        public ICollection<UseCasePreCondition> PreConditions { get; set; }
        public ICollection<UseCasePostCondition> PostConditions { get; set; }
        public SpecificationItem SpecificationItem { get; set; }
        public Guid SpecificationItemId { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
        public int Version { get; set; }
        
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
