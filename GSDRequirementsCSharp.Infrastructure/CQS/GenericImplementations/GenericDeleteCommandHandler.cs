using GSDRequirementsCSharp.Infrastructure.CQS.Interfaces;
using GSDRequirementsCSharp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.CQS.GenericImplementations
{
    class GenericDeleteCommandHandler<TCommand, TModel, TKey> : ICommandHandler<TCommand>
        where TCommand : class, IDeleteCommand<TKey>
        where TModel : class, IEntity<TKey>

    {
        private readonly ICommandToModelConverter<TCommand, TModel> _commandToModel;
        private readonly IRepository<TModel, TKey> _repository;

        public GenericDeleteCommandHandler(ICommandToModelConverter<TCommand, TModel> commandToModel,
                                           IRepository<TModel, TKey> repository)
        {
            _commandToModel = commandToModel;
            _repository = repository;
        }

        public void Handle(TCommand command)
        {
            var model = _repository.Get(command.Id);
            _repository.Remove(model);
        }
    }
}
