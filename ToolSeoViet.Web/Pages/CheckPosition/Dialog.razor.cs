using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo.GetProject;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.CheckPosition {
    public partial class Dialog {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        private List<ProjectDto> items = new();
        private bool loading = false;

        protected override async void OnInitialized() {

            await LoadData();
            StateHasChanged();
        }

        public async Task LoadData() {
            try {
                this.loading = true;
                StateHasChanged();
                this.items = await this.SeoServices.All();
            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } catch (Exception ex) {
                this.Snackbar.Add(ex.Message, Severity.Error);
            } finally {
                loading = false;
                StateHasChanged();
            }
        }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        public void Close(string id) => MudDialog.Close(DialogResult.Ok(id));

        public void SelectProject(ProjectDto project) {
            Close(project.Id);
        }
    }
}
