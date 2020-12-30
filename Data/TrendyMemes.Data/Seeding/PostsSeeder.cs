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
            var images = new Queue<Image>(dbContext.Images);
            var random = new Random();

            var current = 0;
            while (images.Any())
            {
                var author = users[random.Next(0, users.Count)];
                var image = images.Dequeue();

                // Main properties
                var post = new Post
                {
                    Title = $"Post-{current + 1}",
                    Author = author,
                    Image = image,
                };

                // Random tags
                var tagsCount = random.Next(1, 5);
                for (int j = 0; j < tagsCount; j++)
                {
                    var tag = tags[random.Next(0, tags.Count)];

                    if (post.PostTags.Any(pt => pt.TagId == tag.Id))
                    {
                        continue;
                    }

                    var postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tag.Id,
                    };

                    post.PostTags.Add(postTag);

                    current++;
                }

                await dbContext.Posts.AddAsync(post);
            }
        }
    }
}
