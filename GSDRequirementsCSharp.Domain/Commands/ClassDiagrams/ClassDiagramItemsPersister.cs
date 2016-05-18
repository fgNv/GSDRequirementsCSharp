using GSDRequirementsCSharp.Domain.Models;
using GSDRequirementsCSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.Commands
{
    public class ClassDiagramItemsPersister
    {
        private readonly IRepository<ClassDiagramContent, LocaleKey> _classDiagramContentRepository;
        private readonly IRepository<Class, Guid> _classRepository;
        private readonly IRepository<ClassMethod, Guid> _classMethodRepository;
        private readonly IRepository<ClassProperty, Guid> _classPropertyRepository;
        private readonly IRepository<ClassMethodParameter, Guid> _classMethodParameterRepository;
        private readonly IRepository<ClassRelationship, Guid> _classRelationRepository;

        public ClassDiagramItemsPersister(IRepository<ClassDiagramContent, LocaleKey> classDiagramContentRepository,
                                          IRepository<Class, Guid> classRepository,
                                          IRepository<ClassMethod, Guid> classMethodRepository,
                                          IRepository<ClassProperty, Guid> classPropertyRepository,
                                          IRepository<ClassMethodParameter, Guid> classMethodParameterRepository,
                                          IRepository<ClassRelationship, Guid> classRelationRepository)
        {
            _classDiagramContentRepository = classDiagramContentRepository;
            _classRelationRepository = classRelationRepository;
            _classMethodRepository = classMethodRepository;
            _classPropertyRepository = classPropertyRepository;
            _classMethodParameterRepository = classMethodParameterRepository;
            _classRepository = classRepository;
        }

        private void PersistProperty(Class classEntity, PropertyItem propertyData)
        {
            var property = new ClassProperty();
            property.Id = Guid.NewGuid();
            property.Name = propertyData.Name;
            property.Visibility = propertyData.Visibility.Value;
            property.Type = propertyData.Type;

            classEntity.ClassProperties.Add(property);
            _classPropertyRepository.Add(property);
        }

        private void PersistMethod(Class classEntity, MethodItem methodData)
        {
            var method = new ClassMethod();
            method.Id = Guid.NewGuid();
            method.Name = methodData.Name;
            method.Visibility = methodData.Visibility.Value;
            method.ReturnType = methodData.ReturnType;

            foreach (var parameterData in methodData.ClassMethodParameters)
            {
                var parameter = new ClassMethodParameter();
                parameter.Id = Guid.NewGuid();
                parameter.Name = parameterData.Name;
                parameter.Type = parameterData.Type;

                method.ClassMethodParameters.Add(parameter);
                _classMethodParameterRepository.Add(parameter);
            }

            classEntity.ClassMethods.Add(method);
            _classMethodRepository.Add(method);
        }

        private void PersistClass(ClassDiagram classDiagram, ClassItem classData)
        {
            var classEntity = new Class();

            classEntity.Name = classData.Name;
            classEntity.Id = classData.Cell.Id;
            classEntity.X = classData.Cell.Position.X;
            classEntity.Y = classData.Cell.Position.Y;
            classEntity.Type = classData.Type.Value;

            foreach (var propertyData in classData.ClassProperties)
                PersistProperty(classEntity, propertyData);

            foreach (var methodData in classData.ClassMethods)
                PersistMethod(classEntity, methodData);

            classDiagram.Classes.Add(classEntity);
            _classRepository.Add(classEntity);
        }

        public void Persist(ClassDiagram classDiagram, CreateClassDiagramCommand command)
        {
            foreach (var contentItem in command.Contents)
            {
                var classDiagramContent = new ClassDiagramContent();
                classDiagramContent.Id = Guid.NewGuid();
                classDiagramContent.Locale = contentItem.Locale;
                classDiagramContent.Name = contentItem.Name;
                classDiagram.Contents.Add(classDiagramContent);
                _classDiagramContentRepository.Add(classDiagramContent);
            }

            foreach (var classData in command.Classes)
                PersistClass(classDiagram, classData);

            foreach (var relationData in command.Relations)
            {
                var classRelation = new ClassRelationship();
                classRelation.Id = Guid.NewGuid();
                classRelation.SourceId = relationData.SourceId.Value;
                classRelation.SourceMultiplicity = relationData.SourceMultiplicity;
                classRelation.TargetId = relationData.TargetId.Value;
                classRelation.TargetMultiplicity = relationData.TargetMultiplicity;
                classRelation.Type = relationData.Type.Value;

                classDiagram.Relationships.Add(classRelation);
                _classRelationRepository.Add(classRelation);
            }
        }
    }
}
