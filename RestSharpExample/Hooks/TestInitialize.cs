using RestSharpExample.Base;
using System;
using System.Configuration;
using System.Web.Configuration;
using TechTalk.SpecFlow;


namespace RestSharpExample.Hooks
{
    [Binding]
    public class TestInitialize
    {
        private Settings _settings;
        

        public TestInitialize(Settings settings)
        {
            _settings = settings;
        }

        [BeforeScenario]
        public void Initialize() {
            _settings.BaseURL = new Uri(WebConfigurationManager.AppSettings["baseUrl"]);
        }
    }
}
