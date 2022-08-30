using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolSeoViet.Web.Models.Seo;

namespace ToolSeoViet.Web.Pages.CheckPosition
{
    public partial class CheckPosition
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        private List<CheckPositionDto> items = new();
        public List<string> domains = new();
        public CheckPositionDto checkPosition = new();
        public bool success = false;
        public bool loading = true;

        public void LoadData()
        {

        }
    }
}
