using BizCover.Api.Cars.Interfaces;
using BizCover.CarSales.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.CachedRepoApiProxy
{
    public class BizCarRepo : IBizCarRepository
    {
        private readonly HttpClient _client;
        private readonly string _repoUrl = "https://localhost:44391/api/CachedCars";

        public BizCarRepo(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        public async Task<int> AddAsync(BizCoverCar car)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _repoUrl);

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Make", car.Make),
                new KeyValuePair<string, string>("Model", car.Model),
                new KeyValuePair<string, string>("Year", car.Year.ToString()),
                new KeyValuePair<string, string>("CountryManufactured", car.CountryManufactured),
                new KeyValuePair<string, string>("Colour", car.Colour),
                new KeyValuePair<string, string>("Price", car.Price.ToString()),
            };
            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                int.TryParse(await response.Content.ReadAsStringAsync(), out int id);
                return id;
            }
            else
            {
                throw new InvalidOperationException($"Unable add a new car, {response.StatusCode}");
            }
        }

        public async Task<List<BizCoverCar>> GetAllCarsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(_repoUrl);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BizCoverCar>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new InvalidOperationException($"Unable acquire cars list, {response.StatusCode}");
            }
        }

        public async Task<bool> UpdateAsync(BizCoverCar car)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _repoUrl + $"/{car.Id}");

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Make", car.Make),
                new KeyValuePair<string, string>("Model", car.Model),
                new KeyValuePair<string, string>("Year", car.Year.ToString()),
                new KeyValuePair<string, string>("CountryManufactured", car.CountryManufactured),
                new KeyValuePair<string, string>("Colour", car.Colour),
                new KeyValuePair<string, string>("Price", car.Price.ToString()),
            };
            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Unable update a new car, {response.StatusCode}");
            }
        }
    }
}
