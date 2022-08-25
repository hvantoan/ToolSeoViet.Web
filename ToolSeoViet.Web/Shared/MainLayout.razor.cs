using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using ToolSeoViet.Web.Services;
namespace ToolSeoViet.Web.Shared {

    public partial class MainLayout {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthService AuthService { get; set; }

        private bool drawerOpen = true;

        protected override void OnInitialized() {
            long expiredTime = this.AuthService?.CurrentUser?.ExpiredTime ?? 0;
            if (expiredTime < DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()) {
                this.NavigationManager.NavigateTo("login");
            }
        }

        private void DrawerToggle(MouseEventArgs e) {
            this.drawerOpen = !this.drawerOpen;
        }
    }
}