using GSDRequirementsCSharp.Domain.Queries;
using GSDRequirementsCSharp.Domain.Queries.Requirements;
using GSDRequirementsCSharp.Infrastructure.Internationalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class RestoreVersionCommand : IProjectCommand
    {
        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.idIsARequiredField))]
        public Guid? Id { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = nameof(ValidationMessages.versionIsARequiredField))]
        public int? Version { get; set; }

        public static implicit operator UseCaseDiagramDetailQuery(RestoreVersionCommand command)
        {
            return new UseCaseDiagramDetailQuery
            {
                Id = command.Id.Value,
                Version = command.Version.Value
            };
        }
        public static implicit operator ClassDiagramDetailQuery(RestoreVersionCommand command)
        {
            return new ClassDiagramDetailQuery
            {
                Id = command.Id.Value,
                Version = command.Version.Value
            };
        }
        public static implicit operator DetailedRequirementQuery(RestoreVersionCommand command)
        {
            return new DetailedRequirementQuery
            {
                Id = command.Id.Value,
                Version = command.Version.Value
            };
        }
    }
}
