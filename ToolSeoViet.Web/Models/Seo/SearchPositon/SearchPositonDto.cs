using System.Text.Json.Serialization;

namespace ToolSeoViet.Web.Models.Seo.SearchPositon
{
    public class SearchPositonDto
    {
        [JsonIgnore]
        public int STT { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Href { get; set; }
    }
}
