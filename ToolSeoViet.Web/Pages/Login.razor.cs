using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Auth;
using ToolSeoViet.Web.Services;


namespace ToolSeoViet.Web.Pages {

    public partial class Login {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public AuthService AuthService { get; set; }

        private bool loading = false;
        private readonly LoginRequest model = new();

        protected override async Task OnInitializedAsync() {
            await this.AuthService.Logout();
        }

        private async Task OnValidSubmit(EditContext context) {
            try {
                this.loading = true;
                await this.AuthService.Login(this.model);
                this.NavigationManager.NavigateTo("/", true);
            } catch (ManagedException managedEx) {
                this.Snackbar.Add(managedEx.Message, managedEx.ErrorLevel);
            } catch (Exception ex) {
                this.Snackbar.Add(ex.Message, Severity.Error);
            } finally {
                this.loading = false;
            }
        }
    }
}