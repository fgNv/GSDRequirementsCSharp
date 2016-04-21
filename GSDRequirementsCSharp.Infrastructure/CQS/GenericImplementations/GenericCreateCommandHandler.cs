using GSDRequirementsCSharp.Infrastructure.Persistence;
using GSDRequirementsCSharp.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.CQS
{
    public class GenericCreateCommandHandler<TCommand, TModel, TKey> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
        where TModel : class, IEntity<TKey>
    {   
        private readonly ICommandToModelConverter<TCommand, TModel> _commandToModel;
        private readonly IRepository<TModel, TKey> _repository;

        public GenericCreateCommandHandler(ICommandToModelConverter<TCommand, TModel> commandToModel,
                                           IRepository<TModel, TKey> repository)
        {
            _commandToModel = commandToModel;
            _repository = repository;
        }

        public void Handle(TCommand command)
        {
            var model = _commandToModel.BuildModel(command);
            _repository.Add(model);
        }
    }
}
