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
            var vote = await this.GuaranteeVote(postId, userId);
            vote.Value = value;

            // FIXME: Post is not included in the vote query, this is a workaround. Should find the cause and fix it
            var post = this.postsRepository.All()
                .Include(p => p.Votes)
                .FirstOrDefault(p => p.Id == postId);
            post.Rating = post.Votes.Sum(v => v.Value);

            await this.votesRepository.SaveChangesAsync();

            return post.Rating;
        }

        private async Task<Vote> GuaranteeVote(int postId, string userId)
        {
            var vote = this.votesRepository.All()
               .Where(v => v.PostId == postId)
               .Where(v => v.UserId == userId)
               .FirstOrDefault();

            if (vote == null)
            {
                vote = new Vote
                {
                    PostId = postId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
                await this.votesRepository.SaveChangesAsync();
            }

            return vote;
        }
    }
}
