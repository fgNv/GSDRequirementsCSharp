using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Domain.Models.UseCases;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class UseCaseDiagramItemsPersister
    {
        private readonly IRepository<UseCaseDiagramContent, LocaleKey> _classDiagramContentRepository;
        private readonly IRepository<UseCase, Guid> _useCaseRepository;
        private readonly IRepository<UseCaseContent, LocaleKey> _useCaseContentRepository;
        private readonly IRepository<Actor, Guid> _actorRepository;
        private readonly IRepository<ActorContent, LocaleKey> _actorContentRepository;
        private readonly IRepository<UseCasesRelation, Guid> _useCasesRelationRepository;
        private readonly IRepository<UseCaseEntityRelation, Guid> _useCaseEntityRelationRepository;

        public UseCaseDiagramItemsPersister(IRepository<UseCaseDiagramContent, LocaleKey> classDiagramContentRepository,
                                            IRepository<UseCase, Guid> useCaseRepository,
                                            IRepository<UseCaseContent, LocaleKey> useCaseContentRepository,
                                            IRepository<Actor, Guid> actorRepository,
                                            IRepository<ActorContent, LocaleKey> actorContentRepository,
                                            IRepository<UseCasesRelation, Guid> useCasesRelationRepository,
                                            IRepository<UseCaseEntityRelation, Guid> useCaseEntityRelationRepository)
        {
            _classDiagramContentRepository = classDiagramContentRepository;
            _useCaseRepository = useCaseRepository;
            _useCaseContentRepository = useCaseContentRepository;
            _actorRepository = actorRepository;
            _actorContentRepository = actorContentRepository;
            _useCasesRelationRepository = useCasesRelationRepository;
            _useCaseEntityRelationRepository = useCaseEntityRelationRepository;
        }

        //private void PersistProperty(Class classEntity, PropertyItem propertyData)
        //{
        //    var property = new ClassProperty();
        //    property.Id = Guid.NewGuid();
        //    property.Name = propertyData.Name;
        //    property.Visibility = propertyData.Visibility.Value;
        //    property.Type = propertyData.Type;

        //    classEntity.ClassProperties.Add(property);
        //    _actorRepository.Add(property);
        //}

        //private void PersistMethod(Class classEntity, MethodItem methodData)
        //{
        //    var method = new ClassMethod();
        //    method.Id = Guid.NewGuid();
        //    method.Name = methodData.Name;
        //    method.Visibility = methodData.Visibility.Value;
        //    method.ReturnType = methodData.ReturnType;

        //    foreach (var parameterData in methodData.ClassMethodParameters)
        //    {
        //        var parameter = new ClassMethodParameter();
        //        parameter.Id = Guid.NewGuid();
        //        parameter.Name = parameterData.Name;
        //        parameter.Type = parameterData.Type;

        //        method.ClassMethodParameters.Add(parameter);
        //        _useCasesRelationRepository.Add(parameter);
        //    }

        //    classEntity.ClassMethods.Add(method);
        //    _useCaseContentRepository.Add(method);
        //}

        //private void PersistClass(ClassDiagram classDiagram, ClassItem classData)
        //{
        //    var classEntity = new Class();

        //    classEntity.Name = classData.Name;
        //    classEntity.Id = classData.Cell.Id;
        //    classEntity.X = classData.Cell.Position.X;
        //    classEntity.Y = classData.Cell.Position.Y;
        //    classEntity.Type = classData.Type.Value;

        //    foreach (var propertyData in classData.ClassProperties)
        //        PersistProperty(classEntity, propertyData);

        //    foreach (var methodData in classData.ClassMethods)
        //        PersistMethod(classEntity, methodData);

        //    classDiagram.Classes.Add(classEntity);
        //    _useCaseRepository.Add(classEntity);
        //}

        public void Persist(UseCaseDiagram useCaseDiagram, CreateUseCaseDiagramCommand command)
        {
            foreach (var contentItem in command.Contents)
            {
                var useCaseDiagramContent = new UseCaseDiagramContent();
                useCaseDiagramContent.Id = Guid.NewGuid();
                useCaseDiagramContent.Locale = contentItem.Locale;
                useCaseDiagramContent.Name = contentItem.Name;
                useCaseDiagram.Contents.Add(useCaseDiagramContent);
                _classDiagramContentRepository.Add(useCaseDiagramContent);
            }

            //foreach (var classData in command.Classes)
            //    PersistClass(useCaseDiagram, classData);

            //foreach (var relationData in command.Relations)
            //{
            //    var classRelation = new ClassRelationship();
            //    classRelation.Id = Guid.NewGuid();
            //    classRelation.SourceId = relationData.SourceId.Value;
            //    classRelation.SourceMultiplicity = relationData.SourceMultiplicity;
            //    classRelation.TargetId = relationData.TargetId.Value;
            //    classRelation.TargetMultiplicity = relationData.TargetMultiplicity;
            //    classRelation.Type = relationData.Type.Value;

            //    useCaseDiagram.Relationships.Add(classRelation);
            //    _useCaseEntityRelationRepository.Add(classRelation);
            //}
        }
    }
}
