using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.CQS
{
    public interface ICommandToModelConverter<TCommand, TModel>
        where TCommand : ICommand
        where TModel : class
    {
        TModel BuildModel(TCommand command);
    }
}
