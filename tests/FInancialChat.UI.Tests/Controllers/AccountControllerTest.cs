using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Models;
using FinancialChat.UI.Controllers;
using FinancialChat.UI.ViewModels;
using FInancialChat.UI.Tests.Builders;
using FInancialChat.UI.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FInancialChat.UI.Tests.Controllers
{
    public class AccountControllerTest
    {
        private AccountController _accountController;
        private Mock<IAccountService> _mockAccountService;
        private Mock<FakeSignInManager> _mockSignInManager;
        private RegisterViewModel _input;

        [TestInitialize]
        [Fact]
        public void TestInitialize()
        {
            // Arrange
            _input = new NewUserInputBuilder().Build();
            _mockAccountService = new Mock<IAccountService>();
            _mockSignInManager = new Mock<FakeSignInManager>();
            _accountController = new AccountController(_mockSignInManager.Object, _mockAccountService.Object);
        }

        [Fact]
        public async Task RegisterNewUser_RedirectToActionResult_WhenValidModelPosted()
        {
            //Arrange
            TestInitialize();
          
            _mockAccountService.Setup(m => m.RegisterAsync(It.IsAny<UserModel>())).ReturnsAsync(IdentityResult.Success);

            //Act
            var actual = await _accountController.Register(_input);

            //Assert
            var viewResult = Xunit.Assert.IsType<RedirectToActionResult>(actual);
        }

        [Fact]
        public void ValidateModel_ReturnFalse_WhenInvavalidModelPosted_WrongEmail()
        {
            //Arrange
            TestInitialize();
            _input.Email = "wrongemail";
            var context = new ValidationContext(_input, null, null);
            var results = new List<ValidationResult>();


            //Act
            var isModelStateValid = Validator.TryValidateObject(_input, context, results, true);

            //Assert
            Xunit.Assert.False(isModelStateValid);
            Xunit.Assert.True(results.Any());
            Xunit.Assert.True(results.Count == 1);
            Xunit.Assert.Equal("Invalid Email", results.FirstOrDefault().ErrorMessage);
        }

        [Fact]
        public void ValidateModel_ReturnFalse_WhenInvavalidModelPosted_InvalidPassword()
        {
            //Arrange
            TestInitialize();
            _input.Password = "invalidpass";
            var context = new ValidationContext(_input, null, null);
            var results = new List<ValidationResult>();


            //Act
            var isModelStateValid = Validator.TryValidateObject(_input, context, results, true);

            //Assert
            Xunit.Assert.False(isModelStateValid);
            Xunit.Assert.True(results.Any());
            Xunit.Assert.True(results.Count == 1);
            Xunit.Assert.Equal("Invalid Password", results.FirstOrDefault().ErrorMessage);
        }
    }
}
