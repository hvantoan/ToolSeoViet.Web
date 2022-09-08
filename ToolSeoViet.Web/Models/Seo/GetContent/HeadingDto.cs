using System.Collections.Generic;

namespace ToolSeoViet.Web.Models.Seo.GetContent
{
    public class HeadingDto
    {
        public string Href { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public List<TitleDto> Titles { get; set; } = new();
        public List<SubTitleDto> SubTitles { get; set; } =  new();
    }
}
