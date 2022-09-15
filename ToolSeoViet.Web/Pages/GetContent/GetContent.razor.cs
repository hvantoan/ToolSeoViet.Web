using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.GetContent;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.GetContent
{
    public partial class GetContent
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar  Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        private SearchContentDto item = new();
        private bool loading = false;
        private string key = "";
        private async Task SendKeyword(MouseEventArgs args)
        {
            try
            {
                this.loading = true;
                StateHasChanged();
                if (string.IsNullOrEmpty(this.key.Trim()))
                    throw new ManagedException("Từ khóa không được để trống");
                this.item = await this.SeoServices.GetContent(new GetContentRequest() { 
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
        private async Task Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
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
}
