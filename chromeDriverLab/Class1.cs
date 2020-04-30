using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace chromeDriverLab
{
    [TestFixture]
    public class Class1
    {
        private ChromeDriver driver;
        private string url = "https://bfmereforged.org/";

        [SetUp]
        public void Init()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);
        }

        [TearDown]
        public void TheEnd()
        {
            driver.Close();
        }

        [Test]
        public void TestNav()
        {
            driver.FindElementById("menu-item-12045").Click();
            var text = driver.FindElementByCssSelector(".titles h1").GetAttribute("innerText");
            Assert.AreEqual(text, "SUPPORT US");
        }
        /*[Test]
        public void TestLinksImages()
        {
            driver.FindElementById("menu-item-12045").Click();
            var href = driver.FindElementByCssSelector(".elementor-widget-container a").GetAttribute("href");
            Assert.AreEqual(href, url + "isengard/");
        }*/
    }
}
