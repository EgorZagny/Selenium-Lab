using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
        [Test]
        public void TestLinksImages()
        {
            driver.FindElementById("menu-item-11969").Click();
            var links = driver.FindElementsByCssSelector("#works a");
            string[] factions = { "isengard", "factions/elves", "factions/mordor" };
            for(int i = 0; i < factions.Length; i++)
            {
                Assert.AreEqual(links[i].GetAttribute("href"), url + factions[i] +'/');
            }
        }
        [Test]
        public void TestFactionsPage()
        {
            driver.FindElementById("menu-item-11969").Click();
            driver.FindElementByCssSelector("#works a").Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var sections = driver.FindElementsByClassName("elementor-button-text");
            string[] names = { "About faction", "Units", "Heroes", "Buildings", "Spellbook"};
            Assert.AreEqual(sections.Count, names.Length);
            for (int i = 0; i < names.Length; i++)
            {
                Assert.AreEqual(sections[i].GetAttribute("innerText"), names[i]);
            }
        }
    }
}
