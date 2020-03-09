using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Teste
{
    public class PaginaVulcanNovel
    {
        private SeleniumConfigurations _configurations;
        private IWebDriver _driver;

        public PaginaVulcanNovel(SeleniumConfigurations seleniumConfigurations)
        {
            _configurations = seleniumConfigurations;
            ChromeOptions optionsFF = new ChromeOptions();
            optionsFF.AddArgument("--headless");

            _driver = new ChromeDriver(_configurations.CaminhoDriverChrome, optionsFF);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configurations.Timeout);
            _driver.Navigate().GoToUrl(_configurations.UrlPagina);
        }


        //public string ObterNovel()
        //{
        //    var novel = _driver.FindElement(By.ClassName("entry-content"));
        //    return novel.Text;
        //}

        public string ObterNovel()
        {
            var novel = _driver.FindElement(By.ClassName("most-popular-bxslider"));
            return novel.Text;
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}