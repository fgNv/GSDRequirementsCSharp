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
        //public Guid Id { get; set; }
        public ICollection<UseCaseContent> Contents { get; set; }
        public override UseCaseEntityType Type { get { return UseCaseEntityType.UseCase; } }
        public UseCaseEntity UseCaseEntity { get; set; }
        //public Guid UseCaseEntityId { get; set; }

        public UseCase()
        {
            Contents = new HashSet<UseCaseContent>();
        }
    }
}
