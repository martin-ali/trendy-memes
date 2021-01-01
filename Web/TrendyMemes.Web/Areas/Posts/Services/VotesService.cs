namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository, IDeletableEntityRepository<Post> postsRepository)
        {
            this.votesRepository = votesRepository;
            this.postsRepository = postsRepository;
        }

        public async Task<int> VoteOnPostAsync(int postId, string userId, int value)
        {
            var vote = this.votesRepository.All()
                .Include(v => v.Post)
                .FirstOrDefault(v => v.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    PostId = postId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();

            return vote.Post.Rating;
        }

        private async Task DeleteAllWhere(Func<Vote, bool> condition)
        {
            var votes = this.votesRepository.All()
                .Where(condition);

            foreach (var vote in votes)
            {
                this.votesRepository.Delete(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
