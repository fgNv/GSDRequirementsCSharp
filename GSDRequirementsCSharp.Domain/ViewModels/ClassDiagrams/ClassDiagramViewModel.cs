﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ClassDiagramViewModel : IIssueable
    {
        public Guid Id { get; set; }

        public IEnumerable<ClassDiagramContentViewModel> Contents { get; set; }

        public int Identifier { get; set; }

        public int Version { get; set; }

        public int PackageIdentifier { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }

        public static ClassDiagramViewModel FromModel(ClassDiagram model)
        {
            return new ClassDiagramViewModel
            {
                Id = model.Id,
                Identifier = model.Identifier,
                PackageIdentifier = model.SpecificationItem.Package.Identifier,
                Version = model.Version,
                Contents = model.Contents.Select(ClassDiagramContentViewModel.FromModel)
            };
        }
    }
}
