using System.Text.Json.Serialization;
using TuanVu.Management.Web.Models;

namespace ToolSeoViet.Web.Models.Seo
{
    public class CheckIndexDto
    {
        [JsonIgnore]
        public int STT { get; set; }
        public string Href { get; set; }
        public ECheckIndex Status { get; set; }
    }
}
