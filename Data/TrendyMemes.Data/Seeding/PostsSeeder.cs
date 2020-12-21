namespace TrendyMemes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Models;

    public class PostsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Posts.Any())
            {
                return;
            }

            var users = dbContext.Users.ToList();
            var tags = dbContext.Tags.ToList();
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                // Main properties
                var post = new Post
                {
                    Title = $"Post-{i + 1}",
                    Image = this.GetImage(),
                };

                var author = users[random.Next(0, users.Count)];
                author.Posts.Add(post);

                // Random tags
                var tagsCount = random.Next(1, 5);
                for (int j = 0; j < tagsCount; j++)
                {
                    var selectedTag = tags[random.Next(0, tags.Count)];
                    post.Tags.Add(selectedTag);
                }
            }
        }

        private Image GetImage()
        {
            var image = new Image
            {
                Extension = "png",
                Path = "C:\\Users\\marto\\OneDrive\\Desktop\\trendy-memes\\Data\\TrendyMemes.Data\\Seeding\\Images\\11.jpg",
            };

            return image;
        }
    }
}
