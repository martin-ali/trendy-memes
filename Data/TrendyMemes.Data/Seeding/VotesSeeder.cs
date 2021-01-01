namespace TrendyMemes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Models;

    public class VotesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Votes.Any())
            {
                return;
            }

            var users = dbContext.Users.ToList();
            var posts = dbContext.Posts.ToList();
            var random = new Random();

            for (int i = 0; i < 10000; i++)
            {
                var user = users[random.Next(0, users.Count)];
                var post = posts[random.Next(0, posts.Count)];

                if (post.Votes.Any(v => v.UserId == user.Id))
                {
                    continue;
                }

                // Generate random of either -1 or 1, but not 0
                var value = (random.Next(0, 2) * 2) - 1;

                var vote = new Vote
                {
                    PostId = post.Id,
                    UserId = user.Id,
                    Value = value,
                };

                await dbContext.Votes.AddAsync(vote);
            }
        }
    }
}
