using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

using System.Collections.ObjectModel;

using System.IO;

namespace BasketTest
{
    public class BasketTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            Assert.IsTrue(driver.FindElement(By.Id("login-button")).Displayed);

            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");

            driver.FindElement(By.Id("login-button")).Click();
        }

        [Test, Order(1)]
        public void AddItemsToBasketRemoveAndOrder()
        {
            Assert.IsTrue(driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Displayed);
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("add-to-cart-test.allthethings()-t-shirt-(red)")).Displayed);
            driver.FindElement(By.Id("add-to-cart-test.allthethings()-t-shirt-(red)")).Click();

            Assert.That("2", Is.EqualTo(driver.FindElement(By.ClassName("shopping_cart_badge")).Text));

            driver.FindElement(By.Id("shopping_cart_container")).Click();

            var sizeOfList = driver.FindElement(By.ClassName("cart_list")).FindElements(By.ClassName("cart_item"));
            Assert.AreEqual(2, sizeOfList.Count());

            driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();

            sizeOfList = driver.FindElement(By.ClassName("cart_list")).FindElements(By.ClassName("cart_item"));
            Assert.AreEqual(1, sizeOfList.Count());

            driver.FindElement(By.Id("checkout")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("continue")).Displayed);

            driver.FindElement(By.Id("continue")).Click();

            Assert.IsTrue(driver.FindElement(By.ClassName("error-message-container")).Displayed);

            driver.FindElement(By.Id("first-name")).SendKeys("Test name");
            driver.FindElement(By.Id("last-name")).SendKeys("Test last name");
            driver.FindElement(By.Id("postal-code")).SendKeys("1172");

            driver.FindElement(By.Id("continue")).Click();

            Assert.IsTrue(driver.FindElement(By.Id("finish")).Displayed);
            driver.FindElement(By.Id("finish")).Click();

            Assert.IsTrue(driver.FindElement(By.ClassName("pony_express")).Displayed);
            driver.FindElement(By.Id("back-to-products")).Click();
        }

        [Test, Order(2)]
        public void ChangeOrderByTest()
        {
            Assert.IsTrue(driver.FindElement(By.ClassName("product_sort_container")).Displayed);
            driver.FindElement(By.ClassName("product_sort_container")).Click();

            driver.FindElement(By.CssSelector("*[value='lohi']")).Click();
            var itemsList = driver.FindElement(By.ClassName("inventory_list")).FindElements(By.ClassName("inventory_item"));

            Assert.That(itemsList[0].Text, Does.Contain("Sauce Labs Onesie"));
        }

            [TearDown]
        public void TearDown()

        {

            driver.Dispose();

        }
    }
}
