﻿using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class UseCaseDiagramDetailedViewModel
    {
        public IEnumerable<UseCaseDiagramContentViewModel> Contents { get; set; }

        public IEnumerable<UseCaseViewModel> UseCases { get; set; }

        public IEnumerable<ActorViewModel> Actors { get; set; }

        public Guid PackageId { get; set; }

        public Guid Id { get; set; }

        public int Identifier { get; set; }

        public int Version { get; set; }

        public IEnumerable<UseCasesRelationViewModel> UseCasesRelations { get; set; }

        public IEnumerable<UseCaseEntityRelationViewModel> EntitiesRelations { get; set; }

        public static UseCaseDiagramDetailedViewModel FromModel(UseCaseDiagram model,
            IEnumerable<UseCaseViewModel> useCases, IEnumerable<ActorViewModel> actors)
        {
            return new UseCaseDiagramDetailedViewModel
            {
                Id = model.Id,
                Identifier = model.Identifier,
                Actors = actors,
                UseCases = useCases,
                PackageId = model.SpecificationItem.PackageId,
                UseCasesRelations = model.UseCasesRelations
                                         .Select(UseCasesRelationViewModel.FromModel),
                EntitiesRelations = model.EntitiesRelations
                                         .Select(UseCaseEntityRelationViewModel.FromModel),
                Contents = model.Contents
                                .Select(UseCaseDiagramContentViewModel.FromModel),
                Version = model.Version
            };
        }
    }
}
