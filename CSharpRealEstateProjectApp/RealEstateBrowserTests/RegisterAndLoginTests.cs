using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RealEstateApp.Data;
using RealEstateApp.Models;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace RealEstateBrowserTests

{
    [TestClass]
    public class RegisterAndLoginTests
    {
        private const string BaseAddress = "http://localhost:4200";
        private IWebDriver _driver = null!;
        private readonly By _registerAndLoginButtonLocator = By.Id("register");
        private readonly By _signUpButtonLocator = By.Id("overlayBtn");
        private readonly By _registerNameInputLocator = By.Id("registerName");
        private readonly By _registerEmailInputLocator = By.Id("registerEmail");
        private readonly By _registerEmailAgainInputLocator = By.Id("registerEmailAgain");
        private readonly By _registerPasswordInputLocator = By.Id("registerPassword");
        private readonly By _userNameInputLocator = By.Id("userName");
        private readonly By _passwordInputLocator = By.Id("password");
        private readonly By _loginButtonLocator = By.Id("login");
        private readonly By _registerButtonLocator = By.Id("registerButton");
        private readonly By _loginResultLocator = By.Id("toast");

        private IdentityUserDbContext _userDbContext;

        [TestInitialize] 
        public void TestSetup() 
        {
            _driver = new ChromeDriver();
            DbContextOptionsBuilder<IdentityUserDbContext> builder = new DbContextOptionsBuilder<IdentityUserDbContext>();
            builder.UseSqlServer("Server=localhost;Database=RealEstateIdentityDb;User Id=sa;Password=Password123!;TrustServerCertificate=True");
            _userDbContext = new IdentityUserDbContext(builder.Options);
        }

        [TestCleanup]
        public void TestCleanup() 
        {
            _driver.Quit();
            //_driver.Close();
            //_driver.Dispose();
        }
        [TestMethod]
        public void ClearDatabase()
        {
            SqlCommand info = new SqlCommand();
            info.Connection = new SqlConnection("Server = localhost; Database = RealEstateIdentityDb; User Id = sa; Password = Password123!; TrustServerCertificate = True");
            info.CommandType = CommandType.Text;
            info.CommandText = "DELETE FROM AspNetUsers";
            info.Connection.Open();
            info.ExecuteNonQuery();
        }
        [TestMethod]
        public void RegisterTest()
        {
            // Open the webpage
            _driver.Navigate().GoToUrl(BaseAddress);
            Wait(_driver);

            // Click on register/login button
            var registerButton = _driver.FindElement(_registerAndLoginButtonLocator);
            registerButton.Click();

            // Click on sign up button
            var signUpButton = _driver.FindElement(_signUpButtonLocator);
            signUpButton.Click();

            // Enter the userName
            var userNameInput = _driver.FindElement(_registerNameInputLocator);
            userNameInput.SendKeys("username");
            Wait(_driver);

            // Enter the email
            var emailInput = _driver.FindElement(_registerEmailInputLocator);
            emailInput.SendKeys("username@username.com");
            Wait(_driver);

            // Enter the email again
            var emailAgainInput = _driver.FindElement(_registerEmailAgainInputLocator);
            emailAgainInput.SendKeys("username@username.com");
            Wait(_driver);

            // Enter the password
            var passwordInput = _driver.FindElement(_registerPasswordInputLocator);
            passwordInput.SendKeys("Password1!");
            Wait(_driver);

            var results = _driver.FindElement(_loginResultLocator);
            Assert.IsFalse(results.Displayed);

            //Click on login button
            var loginButton = _driver.FindElement(_registerButtonLocator);
            loginButton.Click();

            //Check the result
           _ = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
           .Until(ExpectedConditions.ElementIsVisible(_loginResultLocator));
            Assert.IsTrue(results.Displayed);

            Assert.AreEqual("Your registration was successful!", results.Text);
        }

        [TestMethod]
        public void SignInTest()
        {

            // Open the webpage
            _driver.Navigate().GoToUrl(BaseAddress);
            Wait(_driver);

            // Click on register/login button
            var registerButton = _driver.FindElement(_registerAndLoginButtonLocator);
            registerButton.Click();

            // Enter the userName
            var userNameInput = _driver.FindElement(_userNameInputLocator);
            userNameInput.SendKeys("username");
            Wait(_driver);

            // Enter the password
            var passwordInput = _driver.FindElement(_passwordInputLocator);
            passwordInput.SendKeys("Password1!");
            Wait(_driver);

            var results = _driver.FindElement(_loginResultLocator);
            Assert.IsFalse(results.Displayed);

            // Click on login button
            var loginButton = _driver.FindElement(_loginButtonLocator);
            loginButton.Click();

            // Check the result
            _ = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.ElementIsVisible(_loginResultLocator));
            Assert.IsTrue(results.Displayed);

            Assert.AreEqual("You Logged in Successfully!", results.Text);
        }


        [Conditional("DEBUG")] //'Release'
        private static void Wait(IWebDriver driver)
        {
            new Actions(driver).Pause(TimeSpan.FromSeconds(2)).Perform();
        }
    }
}
