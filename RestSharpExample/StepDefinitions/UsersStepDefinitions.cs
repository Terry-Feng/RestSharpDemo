using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers;
using RestSharpExample.Model;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Web.UI.WebControls;
using TechTalk.SpecFlow;
using RestSharpExample.Base;
using System.Threading;

namespace RestSharpExample.Steps
{
    [Binding]
    public class UsersStepDefinitions
    {
        private Settings _settings;
        public UsersStepDefinitions(Settings settings) => _settings = settings;

        [Given("the user server is up and running")]
        public void GivenTheUserServerIsUpAndRunning()
        {
            _settings.RestClientOptions = new RestClientOptions(_settings.BaseURL);
            _settings.Client = new RestClient(_settings.RestClientOptions);
        }

        [When("create a new user with name (.*) and job (.*)")]
        public void WhenCreateANewUserWithNameAndJob(string name, string job)
        {
            _settings.Request = new RestRequest("api/users", Method.Post);
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddBody(new User
            {
                name = name,
                job = job
            });

            _settings.Response = _settings.Client.Execute<User>(_settings.Request);         
        }

        [When(@"I get the user by id (.*)")]
        public void WhenIGetTheUserById(int id)
        {
            _settings.Request = new RestRequest("/api/users/{id}", Method.Get);
            _settings.Request.AddUrlSegment("id", id);
            _settings.Response = _settings.Client.Execute(_settings.Request);
        }

        [Then(@"the user info returnd and name is (.*)")]
        public void ThenTheUserInfoReturndAndNameIsTom(string name)
        {
            // Slow down the runnnig to demo running paralle
            Thread.Sleep(1000);
            JsonNode data = JsonSerializer.Deserialize<JsonNode>(_settings.Response.Content);
            Assert.That(data["data"]["first_name"].ToString, Is.EqualTo(name), "Name is not correct");
        }


        [Then("the user created successfully")]
        public void ThenTheUserCanBeFoundByTheGetRequest()
        {
            JsonNode data = JsonSerializer.Deserialize<JsonNode>(_settings.Response.Content);
            Assert.That(data["name"].ToString, Is.EqualTo("Tom"), "Name is not correct");
        }
    }
}
