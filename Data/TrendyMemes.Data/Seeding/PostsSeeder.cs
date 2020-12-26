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
            var images = dbContext.Images.ToList();
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                var author = users[random.Next(0, users.Count)];
                var image = images[random.Next(0, images.Count)];

                // Main properties
                var post = new Post
                {
                    Title = $"Post-{i + 1}",
                    Author = author,
                    Image = image,
                };

                // Random tags
                var tagsCount = random.Next(1, 5);
                for (int j = 0; j < tagsCount; j++)
                {
                    var tag = tags[random.Next(0, tags.Count)];
                    var postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tag.Id,
                    };

                    post.Tags.Add(postTag);
                    tag.Posts.Add(postTag);
                }

                await dbContext.Posts.AddAsync(post);
            }
        }
    }
}
