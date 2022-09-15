using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
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

        private SearchContentDto item = new();
        private bool loading = false;
        private string key = "";
        private string strDomain = "";
        private async Task SendKeywordSLI(MouseEventArgs args)
        {
            try
            {
                this.loading = true;
                StateHasChanged();
                if (string.IsNullOrEmpty(this.key.Trim()))
                    throw new ManagedException("Từ khóa không được để trống");
                this.item = await this.SeoServices.GetContent(new GetContentRequest()
                {
                    KeyWord = this.key,
                    Num = 2
                });

            }
            catch (System.Exception ex)
            {
                throw new ManagedException(ex.ToString());
            }
            finally
            {
                this.loading = false;
                StateHasChanged();
            }
        }
    }
}
