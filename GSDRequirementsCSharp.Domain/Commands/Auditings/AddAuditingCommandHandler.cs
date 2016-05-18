using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    class AddAuditingCommandHandler : ICommandHandler<AddAuditingCommand>
    {
        private readonly IRepository<Auditing, Guid> _auditingRepository;

        public AddAuditingCommandHandler(IRepository<Auditing, Guid> auditingRepository)
        {
            _auditingRepository = auditingRepository;
        }

        public void Handle(AddAuditingCommand command)
        {
            var auditing = new Auditing();
            auditing.Id = Guid.NewGuid();
            auditing.ExecutedAt = DateTime.Now;
            auditing.ActivityDescription = command.Description;
            auditing.ProjectId = command.ProjectId.Value;
            auditing.UserId = command.UserId.Value;

            _auditingRepository.Add(auditing);
        }
    }
}
