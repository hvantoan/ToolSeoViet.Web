using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using ToolSeoViet.Web.Models.Auth;

namespace TuanVu.Management.Web.Utilities.Helpers {

    public static class RedirectHelper {

        public static string GetRedirect(CurrentUser currentUser, AuthenticationState authState, Dictionary<string, string> dictClaimRoutes) {
            if (currentUser == null) return "login";
            if (string.IsNullOrWhiteSpace(currentUser.Username)) return "login";
            if (string.IsNullOrWhiteSpace(currentUser.Token)) return "login";

            var user = authState?.User;

            if (user == null) return "login";
            if (!(user.Identity?.IsAuthenticated ?? false)) return "login";
            if (dictClaimRoutes == null) return "login";
            if (dictClaimRoutes.Count == 0) return "login";

            foreach (var item in dictClaimRoutes) {
                if (user.IsInRole(item.Key))
                    return item.Value;
            }

            return "not-authorized";
        }
    }
}