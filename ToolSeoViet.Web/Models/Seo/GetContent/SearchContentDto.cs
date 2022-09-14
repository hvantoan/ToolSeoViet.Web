using System;
using System.Collections.Generic;

namespace ToolSeoViet.Web.Models.Seo.GetContent
{
    public class SearchContentDto
    {
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public List<HeadingDto> Headings { get; set; }
        public List<SLIDto> Sli { get; set; }
    }
}
