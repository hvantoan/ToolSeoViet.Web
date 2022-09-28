using System.ComponentModel.DataAnnotations;

namespace ToolSeoViet.Web.Models.Seo.GetContent
{
    public class SLIDto
    {
        [Required]
        public string KeyWord { get; set; }
        public int Count { get; set; }
    }
}
