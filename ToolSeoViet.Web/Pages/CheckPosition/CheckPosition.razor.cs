using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Web.Exceptions;
using ToolSeoViet.Web.Models.Seo;
using ToolSeoViet.Web.Models.Seo.SearchPositon;
using ToolSeoViet.Web.Services.SeoServices;

namespace ToolSeoViet.Web.Pages.CheckPosition
{
    public partial class CheckPosition
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public SeoService SeoServices { get; set; }

        public List<SearchPositonDto> searchPositons = new();

        public bool success = false;
        public bool loading = false;
        private bool enable = false;
        //two ref input
        private string key;
        private string strDomain = "";

        public void GetDomains(MouseEventArgs args)
        {
            this.key = this.key.Trim();
            if (strDomain != null)
            {
                var keywords = key.Split("\n").ToList();
                if (keywords != null)
                {
                    int index = this.searchPositons.Count == 0 ? 0 : this.searchPositons.Max(o => o.STT);
                    for (int i = 0; i < keywords.Count(); i++)
                    {
                        if (this.searchPositons.Any(o => o.Href == keywords[i]) || string.IsNullOrEmpty(keywords[i].Trim())) continue;
                        index++;
                        this.searchPositons.Add(new SearchPositonDto()
                        {
                            STT = index,
                            Href = strDomain,
                            Key = keywords[i],
                            Name = "None",
                            Position = 0
                        });
                    }
                    //foreach (var item in keywords)
                    //{
                    //    if (this.searchPositons.Any(o => o.Href == item) || string.IsNullOrEmpty(item.Trim())) continue;
                    //    index++;
                    //    this.searchPositons.Add(new SearchPositonDto()
                    //    {
                    //        STT = index,
                    //        Href = strDomain,
                    //        Key = item,
                    //        Name = "None",
                    //        Position = 0
                    //    });
                    //}
                }
            }
        }
        private async Task SendKeyword(MouseEventArgs args)
        {
            try
            {
                this.loading = true;
                StateHasChanged();
                if (string.IsNullOrEmpty(this.key.Trim()) || string.IsNullOrEmpty(this.strDomain.Trim()))
                    throw new ManagedException("Từ khóa hoặc domain không được để trống");
                for (int index = 0; index < searchPositons.Count(); index++)
                {
                    var data = await this.SeoServices.SearchPosition(new SearchIndexRequest() { Domain = this.searchPositons[index].Href, Key = this.searchPositons[index].Key });
                    this.searchPositons[index] = data ?? this.searchPositons[index];
                    this.searchPositons[index].STT = index + 1;
                    StateHasChanged();
                    await Task.Delay(500);
                }

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
