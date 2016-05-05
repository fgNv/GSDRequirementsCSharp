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
        public ICollection<ActorContent> Contents { get; set; }
        public override UseCaseEntityType Type { get { return UseCaseEntityType.Actor; } }
        public UseCaseEntity UseCaseEntity { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Actor()
        {
            Contents = new HashSet<ActorContent>();
        }
    }
}
