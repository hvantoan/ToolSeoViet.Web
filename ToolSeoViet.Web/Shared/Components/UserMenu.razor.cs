using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using ToolSeoViet.Web.Services;

namespace ToolSeoViet.Web.Shared.Components {

    public partial class UserMenu {
        [Inject] public IDialogService DialogService { get; set; }
        [Inject] public AuthService AuthService { get; set; }

        private string username = "";

        protected override void OnInitialized() {
            this.username = this.AuthService?.CurrentUser?.Username ?? "";
        }

        private void ChangePassword(MouseEventArgs args) {
            this.DialogService.Show<ChangePasswordDialog>("Đổi mật khẩu",
               options: new DialogOptions { Position = DialogPosition.TopCenter, MaxWidth = MaxWidth.ExtraSmall });
        }
    }
}