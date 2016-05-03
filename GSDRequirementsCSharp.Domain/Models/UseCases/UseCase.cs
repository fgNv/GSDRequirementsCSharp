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

        public UseCase()
        {
            Contents = new HashSet<UseCaseContent>();
        }
    }
}
