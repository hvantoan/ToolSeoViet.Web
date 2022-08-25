namespace ToolSeoViet.Web.Models.Auth {

    public class CurrentUser {
        public string Token { get; set; }
        public string Username { get; set; }
        public long ExpiredTime { get; set; }
    }
}