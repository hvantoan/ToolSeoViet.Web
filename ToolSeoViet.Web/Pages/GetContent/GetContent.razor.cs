using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.GetContent;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.GetContent {
    public partial class GetContent {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        private SearchContentDto item = new();
        private bool loading = false;
        private bool loadingField = false;
        private string key = "";

        private async Task SendKeyword() {
            if (string.IsNullOrEmpty(this.key.Trim())) {
                this.Snackbar.Add("Từ khóa không được để trống", Severity.Warning);
                return;
            }
            try {
                this.loading = true;
                this.loadingField = true;
                StateHasChanged();

                this.item = await this.SeoServices.GetContent(new GetContentRequest() {
                    KeyWord = this.key,
                    Num = 2
                });
            } catch (ManagedException e) {
                this.Snackbar.Add(e.Message, Severity.Error);
            } catch (Exception ex) {
                this.Snackbar.Add(ex.Message, Severity.Error);
            } finally {
                loading = false;
                this.loadingField = false;
                StateHasChanged();
            }

        }

    }
}
