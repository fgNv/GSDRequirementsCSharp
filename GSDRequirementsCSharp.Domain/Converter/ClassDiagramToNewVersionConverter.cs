using GSDRequirementsCSharp.Domain.Commands.ClassDiagrams;
using GSDRequirementsCSharp.Infrastructure.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Converter
{
    class ClassDiagramToNewVersionConverter : IConverter<ClassDiagram, CreateClassDiagramNewVersionCommand>
    {
        private MethodItem FromModel(ClassMethod model)
        {
            return new MethodItem
            {
                ClassMethodParameters = model.ClassMethodParameters.Select(p => new ParameterItem
                {
                    Name = p.Name,
                    Type = p.Type
                }),
                Name = model.Name,
                ReturnType = model.ReturnType,
                Visibility = model.Visibility
            };
        }

        private PropertyItem FromModel(ClassProperty model)
        {
            return new PropertyItem
            {
                Name = model.Name,
                Type = model.Type,
                Visibility = model.Visibility
            };
        }

        public CreateClassDiagramNewVersionCommand Convert(ClassDiagram input)
        {
            var result = new CreateClassDiagramNewVersionCommand();

            result.Classes = input.Classes.Select(c => new ClassItem
            {
                Cell = new Commands.Cell
                {
                    Id = c.Id,
                    Position = new Commands.Position
                    {
                        X = c.X,
                        Y = c.Y
                    }
                },
                ClassMethods = c.ClassMethods.Select(FromModel),
                ClassProperties = c.ClassProperties.Select(FromModel),
                Name = c.Name,
                Type = c.Type
            });

            result.Contents = input.Contents.Select(c => new ClassDiagramContentItem
            {
                Locale = c.Locale,
                Name = c.Name
            });

            result.Id = input.Id;
            result.PackageId = input.SpecificationItem.PackageId;
            result.Relations = input.Relationships.Select(r => new RelationItem
            {
                SourceId = r.SourceId,
                SourceMultiplicity = r.SourceMultiplicity,
                TargetId = r.TargetId,
                TargetMultiplicity = r.TargetMultiplicity,
                Type = r.Type
            });            

            return result;
        }
    }
}
