using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using ToolSeoViet.Web;
using ToolSeoViet.Web.Models.Auth;
using TuanVu.Management.Web.Utilities.Helpers;

namespace TuanVu.Management.Web {

    public class CustomAuthenticationProvider : AuthenticationStateProvider {
        private readonly ILocalStorageService localStorageService;

        public CustomAuthenticationProvider(ILocalStorageService localStorageService) {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            var currentUser = await this.localStorageService.GetItemAsync<CurrentUser>(Constants.CurrentUser);
            if (string.IsNullOrWhiteSpace(currentUser?.Token)) {
                var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                return anonymous;
            }

            var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(currentUser?.Token), "CustomApiKeyAuth"));
            return new AuthenticationState(userClaimPrincipal);
        }

        public void Notify() {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}