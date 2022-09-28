using Microsoft.AspNetCore.Components;

namespace ToolSeoViet.Web.Shared.Components {
    public partial class LoadingPage {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool Enable { get; set; }
    }
}
