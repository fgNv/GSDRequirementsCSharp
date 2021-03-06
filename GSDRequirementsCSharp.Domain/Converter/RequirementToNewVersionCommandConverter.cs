﻿using GSDRequirementsCSharp.Domain.Commands;
using GSDRequirementsCSharp.Infrastructure.Converter;
using System.Collections.Generic;

namespace GSDRequirementsCSharp.Domain.Converter
{
    class RequirementToNewVersionCommandConverter : IConverter<Requirement, CreateRequirementVersionCommand>
    {
        public CreateRequirementVersionCommand Convert(Requirement input)
        {
            var result = new CreateRequirementVersionCommand();
            result.Difficulty = input.Difficulty;
            result.Id = input.Id;

            var contents = new List<RequirementContentItem>();

            foreach(var content in input.RequirementContents)
            {
                contents.Add(new RequirementContentItem
                {
                    Action = content.Action,
                    Condition = content.Condition,
                    Locale = content.Locale,
                    Subject = content.Subject
                });
            }

            result.Items = contents;
            result.PackageId = input.SpecificationItem.PackageId;
            result.Rank = input.Rank;
            result.RequirementType = input.Type;

            return result;
        }
    }
}
