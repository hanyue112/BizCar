using BizCover.Api.Cars.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace BizCarTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() //initial state, to start, please re-launch BizCover.Api.CachedRepo to your IIS
        {
            string _repoUrl = "https://localhost:44397/api/CarSales";
            HttpClient _client = new HttpClient();
            HttpResponseMessage response = _client.GetAsync(_repoUrl).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<DiscountInfo> cars = JsonConvert.DeserializeObject<List<DiscountInfo>>(response.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(cars.Count == 8);
            Assert.IsTrue(cars.Sum(d => d.RateInPercent) == 9M);
            Assert.IsTrue(cars.Sum(d => d.Amount) == 15039.92M);
        }

        [TestMethod]
        public void TestMethod2() //add a car
        {
            string _repoUrl = "https://localhost:44397/api/CarSales";
            HttpClient _client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _repoUrl);

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Make", "UnitTest1"),
                new KeyValuePair<string, string>("Model", "UnitTest1"),
                new KeyValuePair<string, string>("Year", "1999"),
                new KeyValuePair<string, string>("CountryManufactured", "UnitTest1"),
                new KeyValuePair<string, string>("Colour", "UnitTest1"),
                new KeyValuePair<string, string>("Price", "10000")
            };
            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = _client.SendAsync(request).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestMethod3() //after UnitTest1 added
        {
            string _repoUrl = "https://localhost:44397/api/CarSales";
            HttpClient _client = new HttpClient();
            HttpResponseMessage response = _client.GetAsync(_repoUrl).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<DiscountInfo> cars = JsonConvert.DeserializeObject<List<DiscountInfo>>(response.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(cars.Count == 9);
            Assert.IsTrue(cars.Sum(d => d.RateInPercent) == 9M + 1M); //1999, should credit 10% of that car
            Assert.IsTrue(cars.Sum(d => d.Amount) == 15039.92M + 1000M + (10000M * 0.08M)); // 10K list discount 5M + more than 2 cars discount 3M
        }

        [TestMethod]
        public void TestMethod4() //add a car
        {
            string _repoUrl = "https://localhost:44397/api/CarSales";
            HttpClient _client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _repoUrl);

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Make", "UnitTest2"),
                new KeyValuePair<string, string>("Model", "UnitTest2"),
                new KeyValuePair<string, string>("Year", "2009"),
                new KeyValuePair<string, string>("CountryManufactured", "UnitTest2"),
                new KeyValuePair<string, string>("Colour", "UnitTest2"),
                new KeyValuePair<string, string>("Price", "10000")
            };
            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = _client.SendAsync(request).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestMethod5() //after UnitTest2 added
        {
            string _repoUrl = "https://localhost:44397/api/CarSales";
            HttpClient _client = new HttpClient();
            HttpResponseMessage response = _client.GetAsync(_repoUrl).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<DiscountInfo> cars = JsonConvert.DeserializeObject<List<DiscountInfo>>(response.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(cars.Count == 10);
            Assert.IsTrue(cars.Sum(d => d.RateInPercent) == 9M + 1M); //2009, no more discount should be applied on this car
            Assert.IsTrue(cars.Sum(d => d.Amount) == 15039.92M + 1000M + (20000M * 0.08M));
        }

        [TestMethod]
        public void TestMethod6() //Update UnitTest2, update price from 10K to 20K
        {
            string _repoUrl = "https://localhost:44397/api/CarSales/8";
            HttpClient _client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, _repoUrl);

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Make", "UnitTest2"),
                new KeyValuePair<string, string>("Model", "UnitTest2"),
                new KeyValuePair<string, string>("Year", "2009"),
                new KeyValuePair<string, string>("CountryManufactured", "UnitTest2"),
                new KeyValuePair<string, string>("Colour", "UnitTest2"),
                new KeyValuePair<string, string>("Price", "20000")
            };
            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage response = _client.SendAsync(request).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
           
        }

        [TestMethod]
        public void TestMethod7() //after UnitTest2 added
        {
            string _repoUrl = "https://localhost:44397/api/CarSales";
            HttpClient _client = new HttpClient();
            HttpResponseMessage response = _client.GetAsync(_repoUrl).Result;
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<DiscountInfo> cars = JsonConvert.DeserializeObject<List<DiscountInfo>>(response.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(cars.Count == 10);
            Assert.IsTrue(cars.Sum(d => d.RateInPercent) == 9M + 1M);
            Assert.IsTrue(cars.Sum(d => d.Amount) == 15039.92M + 1000M + (30000M * 0.08M));
        }
        //more to fully cover this porject..................
    }
}