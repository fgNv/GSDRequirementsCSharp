using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSDRequirementsCSharp.Persistence.Commands.Users.SaveUserCommand;
using GSDRequirementsCSharp.Tests.DependencyInjection;
using GSDRequirementsCSharp.Infrastructure.Validation;

namespace GSDRequirementsCSharp.Tests
{
    [TestClass]
    public class UserCommandValidationTests
    {
        [TestMethod]
        public void ValidCreateUserCommandShouldSucceed()
        {
            var command = new CreateUserCommand();

            command.Email = "teste@teste.com";
            command.Login = "teste";
            command.MobilePhone = "9988887777";
            command.Name = "teste";
            command.Password = "teste";
            command.Phone = "77666655555";        

            var validator = ContainerExtensions.GetInstance<IValidator>();
            var errors = validator.Validate(command);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void CreateUserCommandWithLettersInPhoneShouldFail()
        {
            var command = new CreateUserCommand();

            command.Email = "teste@teste.com";
            command.Login = "teste";
            command.MobilePhone = "9988887777";
            command.Name = "teste";
            command.Password = "teste";
            command.Phone = "776666b5555";

            var validator = ContainerExtensions.GetInstance<IValidator>();
            var errors = validator.Validate(command);
            Assert.AreEqual(1, errors.Count());
        }
    }
}
