namespace GSDRequirementsCSharp.Domain
{
    using Infrastructure.Persistence;
    using Models;
    using Models.UseCases;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Actor : UseCaseEntity
    {
        //public Guid Id { get; set; }
        public ICollection<ActorContent> Contents { get; set; }
        public override UseCaseEntityType Type { get { return UseCaseEntityType.Actor; } }
        public UseCaseEntity UseCaseEntity { get; set; }
       // public Guid UseCaseEntityId { get; set; }

        public Actor()
        {
            Contents = new HashSet<ActorContent>();
        }
    }
}
