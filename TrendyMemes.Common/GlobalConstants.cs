namespace TrendyMemes.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "TrendyMemes";

        public const string AdministratorRoleName = "Administrator";

        public const string TagsSeparator = " ";

        public const string ImagesDirectory = "images";

        public const string AdministrationArea = "Administration";

        public const string CommentsArea = "Comments";

        public const string IdentityArea = "Identity";

        public const string PostsArea = "Posts";

        public const string SettingsArea = "Settings";

        public const string TagsArea = "Tags";

        public const double TopPostsPercentageInTrendyCategory = 10;

        public const double TopPostsPercentageInRisingCategory = 40;

        public const double PostsPercentageInNewCategory = 50;

        public static readonly string[] AllowedImageExtensions = new[] { "png", "jpg" };
    }
}
