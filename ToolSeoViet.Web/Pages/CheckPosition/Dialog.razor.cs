using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models.Seo.GetProject;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.CheckPosition
{
    public partial class Dialog
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        public List<ProjectDto> items = new();

        protected override async void OnInitialized()
        {
            await LoadData();
            StateHasChanged();
        }

        public async Task LoadData() {
            this.items = await this.SeoServices.All();
        }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        void Close() => MudDialog.Close(DialogResult.Ok(true));

        public void SelectProject(ProjectDto project)
        {
            //TODO: Call api get project detail
            string test = null;
            
        }
    }
}
