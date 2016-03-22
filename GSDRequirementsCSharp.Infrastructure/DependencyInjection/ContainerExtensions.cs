﻿using GSDRequirementsCSharp.Infrastructure.CQS;
using GSDRequirementsCSharp.Infrastructure.Validation;
using SimpleInjector;
using SimpleInjector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.DependencyInjection
{
    public static class ContainerExtensions
    {
        public static void RegisterInfrastructureDependencies(this Container container, 
                                                              Lifestyle lifestyle)
        {
            container.RegisterDecorator(typeof(ICommandHandler<>),
                                        typeof(CommandHandlerValidationDecorator<>),
                                        lifestyle);
        }
    }
}
