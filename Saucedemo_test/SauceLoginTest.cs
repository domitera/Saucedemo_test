using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

using System.Collections.ObjectModel;

using System.IO;

namespace SauceLoginTest
{
    public class SauceLoginTest
    {

        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        [Test, Order(1)]
        public void LogInCorrectly()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));

            Assert.IsTrue(driver.FindElement(By.Id("login-button")).Displayed);

            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");

            driver.FindElement(By.Id("login-button")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("inventory_container")).Displayed);
            
            driver.FindElement(By.Id("react-burger-menu-btn")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("logout_sidebar_link")));
            driver.FindElement(By.Id("logout_sidebar_link")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("login-button")).Displayed);
        }

        [Test, Order(2)]
        public void LockedOutUser()
        {

            driver.FindElement(By.Id("user-name")).SendKeys("locked_out_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");

            driver.FindElement(By.Id("login-button")).Click();

            Assert.IsTrue(driver.FindElement(By.ClassName("error-message-container")).Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}