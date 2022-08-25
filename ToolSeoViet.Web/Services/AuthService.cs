using Blazored.LocalStorage;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models;
using ToolSeoViet.Web.Models.Auth;
using ToolSeoViet.Web.Services.Common;

namespace ToolSeoViet.Web.Services {

    public class AuthService : BaseService {
        public string MerchantCode { get; private set; }
        public CurrentUser CurrentUser { get; private set; }

        private readonly ILocalStorageService localStorageService;

        public AuthService(HttpService httpService, ILocalStorageService localStorageService) : base(httpService) {
            this.localStorageService = localStorageService;
        }

        public async Task Initialize() {
            this.CurrentUser = await this.localStorageService.GetItemAsync<CurrentUser>(Constants.CurrentUser);
        }

        public async Task Login(LoginRequest body) {
            var response = await this.httpService.PostAsync<BaseResponse<CurrentUser>>("/api/auth/login", body);
            ValidateResponse(response);

            this.CurrentUser = response.Data;
            await this.localStorageService.SetItemAsync(Constants.CurrentUser, this.CurrentUser);
        }

        public async Task Logout() {
            this.CurrentUser = null;
            await this.localStorageService.RemoveItemAsync(Constants.CurrentUser);
        }
    }
}