namespace TrendyMemes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Models;

    public class CommentsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Comments.Any())
            {
                return;
            }

            var users = dbContext.Users.ToList();
            var posts = dbContext.Posts.ToList();
            var random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                // Do title and image
                var author = users[random.Next(0, users.Count)];
                var post = posts[random.Next(0, posts.Count)];

                var comment = new Comment
                {
                    Author = author,
                    Post = post,
                    Content = $"Comment-{i + 1}",
                };

                post.Comments.Add(comment);
                author.Comments.Add(comment);
            }
        }
    }
}
