using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpExample.Model;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;

namespace RestSharpExample
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var client = new RestClient("https://reqres.in/");

            string path = "api/users/{userId}";

            var request = new RestRequest(path, Method.Get);

            request.AddUrlSegment("userId", 2);

            var content = client.Execute(request).Content;

            var data = JsonSerializer.Deserialize<JsonNode>(content);

            Console.WriteLine(data["data"]["email"]);

            Assert.That(data["data"]["first_name"].ToString, Is.EqualTo("Janet"), "Name is not correct");
        }

        [Test]
        public void PostWithAnoymousBody()
        {
            var client = new RestClient("https://reqres.in/");

            string path = "api/users";

            var request = new RestRequest(path, Method.Post);

            // request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                name = "morpheus",
                job = "leader"
            });

            var content = client.Execute(request).Content;

            var data = JsonSerializer.Deserialize<JsonNode>(content);

            Console.WriteLine(data["name"]);

            Assert.That(data["name"].ToString, Is.EqualTo("morpheus"), "Name is not correct");
        }


        [Test]
        public void PostWithClassBody()
        {
            var client = new RestClient("https://reqres.in/");

            string path = "api/users";

            var request = new RestRequest(path, Method.Post);

            // request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            User
            {
                name = "Tom",
                job = "leader"
            });

            var response = client.Execute<User>(request);

            var name = response.Data.name;

            Assert.That(name, Is.EqualTo("Tom"), "Name is not correct");
        }

        [Test]
        public async void PostWithAsync()
        {
            var client = new RestClient("https://reqres.in/");

            string path = "api/users";

            var request = new RestRequest(path, Method.Post);

            var cancellationTokenSource = new CancellationTokenSource();

            var testFolder = "TestData/UserData.json";

            var testD = JsonSerializer.Deserialize<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testFolder)));

            request.AddBody(testD);

            var response = await client.ExecuteAsync<User>(request, cancellationTokenSource.Token);

            var name = response.Data.name;

            Assert.That(name, Is.EqualTo("Tom"), "Name is not correct");
        }

        [Test]
        public void AuthTest()
        {
            var options = new RestClientOptions();
            options.Authenticator = new JwtAuthenticator("1234567890");

            options.BaseUrl = new Uri("https://httpbin.org/");

            var client = new RestClient(options);

            string path = "/bearer";

            var request = new RestRequest(path, Method.Get);

            var response = client.Execute(request);
            //var data = JsonSerializer.Deserialize<JsonNode>(response.Content);

            Assert.That(response.StatusCode.ToString, Is.EqualTo("OK"), "Status code is not 200");
        }
    }
}
