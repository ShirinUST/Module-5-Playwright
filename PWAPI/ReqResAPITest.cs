using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PWAPI
{
    internal class ReqResAPITest
    {
        IAPIRequestContext requestContext;

        [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            requestContext = await playwright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://reqres.in/api/"
                });
        }
        [Test]
        public async Task GetAllUsers()
        {
            var getresponse = await requestContext.GetAsync(url: "users?page=2");

            await Console.Out.WriteLineAsync("Res : \n"+getresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + getresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + getresponse.StatusText);

            Assert.That(getresponse.Status.Equals(200));
            Assert.That(getresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body : \n"+ responseBody.ToString());
        }

        [Test]
        [TestCase(23)]
        public async Task GetSingleUserNotFound(int uid)
        {
            var getresponse = await requestContext.GetAsync(url: "users/"+uid);

            await Console.Out.WriteLineAsync("Res : \n" + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + getresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + getresponse.StatusText);

            Assert.That(getresponse.Status.Equals(404));
            Assert.That(getresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body : \n" + responseBody.ToString());

            Assert.That(responseBody.ToString(), Is.EqualTo("{}"));
        }

        [Test]
        [TestCase(2,"John","Engineer")]
        public async Task PostUser(int uid,string nm,string jb)
        {
            var postData = new
            {
                name = nm,
                job = jb
            };
            var jsonData=System.Text.Json.JsonSerializer.Serialize(postData);

            var postresponse = await requestContext.PostAsync(url: "users/"+uid,
                new APIRequestContextOptions
                {
                    Data=jsonData
                });

            await Console.Out.WriteLineAsync("Res : \n" + postresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + postresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + postresponse.StatusText);

            Assert.That(postresponse.Status.Equals(201));
            Assert.That(postresponse, Is.Not.Null);
        }
        [Test]
        [TestCase(2,"Jon","Engineer")]
        public async Task PutUser(int uid,string nm,string jb)
        {
            var postData = new
            {
                name = nm,
                job = jb
            };
            var jsonData = System.Text.Json.JsonSerializer.Serialize(postData);

            var putresponse = await requestContext.PutAsync(url: "users/"+uid,
                new APIRequestContextOptions
                {
                    Data = jsonData
                });

            await Console.Out.WriteLineAsync("Res : \n" + putresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + putresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + putresponse.StatusText);

            Assert.That(putresponse.Status.Equals(200));
            Assert.That(putresponse, Is.Not.Null);
        }
        [Test]
        [TestCase(2)]
        public async Task DeleteUser(int uid)
        {
            var deleteresponse = await requestContext.DeleteAsync(url: "users/"+uid);

            await Console.Out.WriteLineAsync("Res : \n" + deleteresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + deleteresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + deleteresponse.StatusText);

            Assert.That(deleteresponse.Status.Equals(204));
            Assert.That(deleteresponse, Is.Not.Null);
        }
    }
}
