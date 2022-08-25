using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Auth;

namespace ToolSeoViet.Web.Services.Common {

    public class HttpService {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly NavigationManager navigationManager;
        private readonly ISnackbar snackbar;

        public HttpService(
            HttpClient httpClient,
            ILocalStorageService localStorageService,
            NavigationManager navigationManager,
            ISnackbar snackbar
        ) {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.navigationManager = navigationManager;
            this.snackbar = snackbar;
        }

        public async Task<T> PostAsync<T>(string endpoint, object body) {
            var json = await this.ExecuteAsync(HttpMethod.Post, endpoint, body);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> GetAsync<T>(string endpoint) {
            var json = await this.ExecuteAsync(HttpMethod.Get, endpoint);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task<string> ExecuteAsync(HttpMethod httpMethod, string endpoint, object body = null, TimeSpan? timeout = null) {
            var request = new HttpRequestMessage(httpMethod, endpoint);
            if (body != null) {
                string json = JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            request.Headers.Add("Access-Control-Allow-Origin", "*");
            request.Headers.Add("Access-Control-Allow-Methods", "*");
            request.Headers.Add("Access-Control-Allow-Headers", "*");

            var currentUser = await this.localStorageService.GetItemAsync<CurrentUser>(Constants.CurrentUser);
            if (currentUser != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", currentUser.Token);

            if (timeout != null)
                this.httpClient.Timeout = timeout.Value;

            var response = await this.httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) {
                if (response.StatusCode == HttpStatusCode.Unauthorized) {
                    this.snackbar.Add("Bạn vui lòng đăng nhập lại.", Severity.Info);
                    this.navigationManager.NavigateTo("login", true);
                    throw new ManagedException(response.StatusCode.ToString());
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new ManagedException(error);
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}