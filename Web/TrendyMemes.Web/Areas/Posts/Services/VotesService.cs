namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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
            var sumOfPreviousVotes = this.GetSumOfPreviousVoteValues(postId, userId);
            await this.DeleteAllWhere(v => v.UserId == userId && v.PostId == postId);

            var vote = new Vote
            {
                PostId = postId,
                UserId = userId,
                Value = value,
            };

            var post = this.postsRepository.All()
                .FirstOrDefault(p => p.Id == postId);

            post.Rating -= sumOfPreviousVotes;
            post.Rating += value;

            await this.votesRepository.AddAsync(vote);
            await this.votesRepository.SaveChangesAsync();

            return post.Rating;
        }

        private int GetSumOfPreviousVoteValues(int postId, string userId)
        {
            var sum = this.votesRepository.All()
                           .Where(v => v.PostId == postId && v.UserId == userId)
                           .Sum(v => v.Value);

            return sum;
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
