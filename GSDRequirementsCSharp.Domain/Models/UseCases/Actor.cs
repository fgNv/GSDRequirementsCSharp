namespace GSDRequirementsCSharp.Domain
{
    using Models;
    using System;
    using System.Collections.Generic;

    public class Actor : UseCaseEntity
    {
        public override Guid Id { get; set; }
        public ICollection<ActorContent> Contents { get; set; }
        public override UseCaseEntityType Type { get { return UseCaseEntityType.Actor; } }

        public Actor()
        {
            Contents = new HashSet<ActorContent>();
        }
    }
}
