﻿using ToolSeoViet.Web.Models.Seo.GetProject;

namespace ToolSeoViet.Web.Models.Seo
{
    public class CheckIndexRequest {
        public string Href { get; set; }
    }

    public class GetContentRequest
    {
        public string KeyWord { get; set; }
        public int Num { get; set; }
    }

    public class SearchIndexRequest
    {
        public string Key { get; set; }
        public string Domain { get; set; }
    }

    public class SearchPositionRequest {
        public ProjectDetailDto ProjectDetail { get; set; }
        public string Domain { get; set; }
    }
}
