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
        private int currentImage = 1;

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
                // Do title and image
                var author = users[random.Next(0, users.Count)];

                var post = new Post
                {
                    Title = $"Post-{i + 1}",
                    Author = author,
                    Image = this.GetImage(),
                };

                // Add count of random rags
                var tagsCount = random.Next(1, 10);
                var selectedTags = new HashSet<Tag>();

                for (int j = 0; j < tagsCount; j++)
                {
                    var selectedTag = tags[random.Next(0, tags.Count)];
                    selectedTags.Add(selectedTag);
                }

                post.Tags = selectedTags;
                await dbContext.Posts.AddAsync(post);
            }
        }

        private Image GetImage()
        {
            var image = new Image
            {
                Extension = "png"
            };

            return image;
        }
    }
}
