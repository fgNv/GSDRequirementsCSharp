using GSDRequirementsCSharp.Domain.ViewModels.UseCases;
using GSDRequirementsCSharp.Infrastructure.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSDRequirementsCSharp.Web.Api
{
    public class UseCaseController : ApiController
    {
        private readonly IQueryHandler<Guid, UseCaseViewModel> _useCaseByIdQueryHandler;

        public UseCaseController(IQueryHandler<Guid, UseCaseViewModel> useCaseByIdQueryHandler)
        {
            _useCaseByIdQueryHandler = useCaseByIdQueryHandler;
        }

        public UseCaseViewModel Get(Guid id)
        {
            return _useCaseByIdQueryHandler.Handle(id);
        }
    }
}