using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.GetContent;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.SLI
{
    public partial class SLI
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        private string strContent = "";
        char[] separator = { ' ' };
        SLIDto model = new SLIDto();
        private SearchContentDto item = new();
        private bool loading = false;
        private string key = "";
        private string strDomain = "";
        private bool loadingField = false;
        private bool success;

        private async Task SendKeywordSLI(MouseEventArgs args)
        {
            try
            {
                this.loading = true;
                this.loadingField = true;
                StateHasChanged();
                if (string.IsNullOrEmpty(this.model?.KeyWord?.Trim()))
                    throw new ManagedException("Từ khóa không được để trống");
                this.item = await this.SeoServices.GetContent(new GetContentRequest()
                {
                    KeyWord = this.model.KeyWord,
                    Num = 2
                });

            }
            catch (ManagedException ex)
            {
                this.Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                this.loading = false;
                this.loadingField = false;
                StateHasChanged();
            }
        }

        private void OnValidSubmit(EditContext context) {
            success = true;
            StateHasChanged();
        }
    }
}
