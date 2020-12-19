namespace TrendyMemes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TrendyMemes.Data.Models;

    public class TagsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tags.Any())
            {
                return;
            }

            for (int i = 0; i < 100; i++)
            {
                var tag = new Tag
                {
                    Name = $"Tag-{i + 1}",
                };

                await dbContext.Tags.AddAsync(tag);
            }
        }
    }
}
