using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToolSeoViet.Web.Services;

namespace ToolSeoViet.Web.Pages.CheckIndex {
    public partial class CheckIndex {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public AuthService AuthService { get; set; }

    }
}
