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
        public ICollection<UseCaseContent> Contents { get; set; }
        public override UseCaseEntityType Type { get { return UseCaseEntityType.UseCase; } }
        public UseCaseEntity UseCaseEntity { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public UseCase()
        {
            Contents = new HashSet<UseCaseContent>();
        }
    }
}
