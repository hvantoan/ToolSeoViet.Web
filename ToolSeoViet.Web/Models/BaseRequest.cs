using ToolSeoViet.Web;

namespace TuanVu.Management.Web.Models {

    public class BaseGetRequest {
        public string Id { get; set; }
    }

    public class BaseListRequest {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = Constants.DefaultPageSize;
        public bool IsCount { get; set; } = true;
        public string SearchText { get; set; }
    }
}