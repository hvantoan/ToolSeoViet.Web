namespace ToolSeoViet.Web.Models.Seo.GetProject
{
    public class ProjectDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; } = 0;
        public int BestPosition { get; set; } = 0;
        public string Url { get; set; } = "";
        public string ProjectId { get; set; }
        public ProjectDto Project { get; set; }
    }
}
