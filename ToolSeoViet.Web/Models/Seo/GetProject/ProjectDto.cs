using System.Collections.Generic;

namespace ToolSeoViet.Web.Models.Seo.GetProject
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public List<ProjectDetailDto> ProjectDetails { get; set; } = new();
    }
}
