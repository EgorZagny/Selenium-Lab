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
    public class TestClass
    {
        private ChromeDriver driver;
        private readonly string url = "https://bfmereforged.org/";

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
            for (int i = 0; i < factions.Length; i++)
            {
                Assert.AreEqual(links[i].GetAttribute("href"), url + factions[i] + '/');
            }
        }
        [Test]
        public void TestFactionsPage()
        {
            driver.FindElementById("menu-item-11969").Click();
            driver.FindElementByCssSelector("#works a").Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var sections = driver.FindElementsByClassName("elementor-button-text");
            string[] names = { "About faction", "Units", "Heroes", "Buildings", "Spellbook" };
            Assert.AreEqual(sections.Count, names.Length);
            for (int i = 0; i < names.Length; i++)
            {
                Assert.AreEqual(sections[i].GetAttribute("innerText"), names[i]);
            }
        }
        [Test]
        public void TestSearch1()
        {
            driver.FindElementById("search-button").Click();
            var input = driver.FindElementById("s1");
            input.Click();
            input.SendKeys("isengard");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var results = driver.FindElementsByCssSelector("#search-results-header a");
            bool b = false;
            foreach (var result in results)
            {
                if (result.GetAttribute("href") == url + "isengard/")
                {
                    Assert.IsTrue(true);
                    b = true;
                    break;
                }
            }
            if (!b) Assert.IsTrue(false);
        }
        [Test]
        public void TestSearch2()
        {
            driver.FindElementById("search-button").Click();
            var input = driver.FindElementById("s1");
            input.Click();
            input.SendKeys("dorwinion");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            var result = driver.FindElementByCssSelector("#search-results-header h2");
            Assert.AreEqual(result.GetAttribute("innerText"), "No results found.");
        }
        [Test]
        public void TestMoreInfoSocial()
        {
            driver.FindElementById("side-menu-switch").Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var links = driver.FindElementsByCssSelector("#a13-social-icons-3 a");
            string[] social = { "www.facebook.com", "bfmereforged.org/support-us/", "www.youtube.com" };
            for (int i = 0; i < social.Length; i++)
            {
                Assert.IsTrue(links[i].GetAttribute("href").Contains(social[i]));
            }
        }
        [Test]
        public void TestMoreInfoCateg()
        {
            driver.FindElementById("side-menu-switch").Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var links = driver.FindElementsByCssSelector("#categories-5 a");
            string[] categories = { "News", "Others", "Post", "Uncategorized" };
            for (int i = 0; i < categories.Length; i++)
            {
                Assert.AreEqual(links[i].GetAttribute("innerText"), categories[i]);
            }
        }
        [Test]
        public void TestMoreInfoMeta()
        {
            driver.FindElementById("side-menu-switch").Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var links = driver.FindElementsByCssSelector("#meta-5 a");
            string[] categories = { "Log in", "Entries RSS", "Comments RSS", "WordPress.org" };
            for (int i = 0; i < categories.Length; i++)
            {
                Assert.AreEqual(links[i].GetAttribute("innerText"), categories[i]);
            }
        }
    }
}
