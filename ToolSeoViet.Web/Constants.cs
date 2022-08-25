namespace ToolSeoViet.Web {

    public static class Constants {
        public const int DefaultPageSize = 10;
        public static readonly int[] PageSizes = new int[] { 10, 25, 50 };

        public const string CurrentUser = "__user__";
    }

    public static class PermissionClaim {
        public const string SEO = "SEO";
        public const string SEO_Project = "SEO.Project";
        public const string SEO_ProjectSave = "SEO.Project.Save";
        public const string SEO_Setting = "SEO.Setting";
        public const string SEO_Setting_User = "SEO.Setting.User";
        public const string SEO_Setting_UserReset = "SEO.Setting.User.Reset";
        public const string SEO_Setting_UserSave = "SEO.Setting.User.Save";
    }
}