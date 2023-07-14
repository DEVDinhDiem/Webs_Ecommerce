﻿using Ecommerce.AdminApp.ApiIntegration;
using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace Ecommerce.AdminApp.Services
{
	public class UserApiClient : IUserApiClient
	{
		private readonly IConfiguration _configuration;

		private readonly IHttpClientFactory _httpClientFactory;

		public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}
		public async Task<string> Authenticate(LoginRequest request)
		{
			//var url = URL;
			//var data = new FormUrlEncodedContent(new[] {
			//		new KeyValuePair<string, string>("client_id", FRAppConfigKeys.GetValue("ClientId")),
			//		new KeyValuePair<string, string> ("client_secret", FRAppConfigKeys.GetValue("ClientSecret")),
			//		new KeyValuePair<string, string>("grant_type", FRAppConfigKeys.GetValue("GrantType") ),
			//		new KeyValuePair<string, string>("username", FRAppConfigKeys.GetValue("UserName")),
			//		new KeyValuePair<string, string> ("password", FRAppConfigKeys.GetValue("Password"))
			//	}
			//);
			//HttpResponseMessage response = client.PostAsync(url, data).Result;
			//var result = response.Content.ReadAsStringAsync().Result;
			//Dictionary<string, string> tokenDictionary =
			//   JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
			//string token = tokenDictionary["access_token"];


			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri("https://localhost:7201/");
			var response = await client.PostAsync("api/Users/Authenticate", httpContent);
			var token = await response.Content.ReadAsStringAsync();

			//Dictionary<string, string> tokenDictionary =
			//   JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
			//string token = tokenDictionary["access_token"];

			return token;
		}

		public async Task<PagedResult<UserVm>> GetUsersPaging(GetUserPagingRequest request)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);
			var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
				$"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
			var body = await response.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<PagedResult<UserVm>>(body);
			return users;
		}

        public async Task<bool> RegisterUser(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/users", httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
