using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Services.SeoServices;
using TuanVu.Management.Web.Models;

namespace ToolSeoViet.Web.Pages.CheckIndex
{
    public partial class CheckIndex
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        public List<CheckIndexDto> checkIndices = new();

        private bool loading = false;

        private string strDomain;
        public void AddManyDomain(MouseEventArgs args)
        {
            loading = true;
            var domains = strDomain.Split("\n").ToList();
            int index = this.checkIndices.Count == 0 ? 0 : this.checkIndices.Max(o => o.STT);
            foreach (var item in domains)
            {
                if (this.checkIndices.Any(o=>o.Href == item) || string.IsNullOrEmpty(item.Trim())) continue;
                index++;
                this.checkIndices.Add(new CheckIndexDto()
                {
                    STT = index,
                    Href = item,
                    Status = ECheckIndex.None
                });
            }
            StateHasChanged();
            loading = false;
        }

        private async Task SendDomain(MouseEventArgs args)
        {
            try
            {
                this.loading = true;
                StateHasChanged();
                for (int index = 0; index < this.checkIndices.Count; index++)
                {
                    var data = await this.SeoServices.CheckIndex(new CheckIndexRequest() { Href = this.checkIndices[index].Href });
                    if (data != null) { 
                        this.checkIndices[index] = data;
                        this.checkIndices[index].STT = index + 1;
                        StateHasChanged();
                        await Task.Delay(1000);
                    }
                }
            }
            catch (System.Exception)
            {

            }
            finally
            {
                this.loading = false;
                StateHasChanged();
            }
        }


    }
}
