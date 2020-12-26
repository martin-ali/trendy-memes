namespace TrendyMemes.Web.Areas.Posts.Services
{
    using System.Threading.Tasks;

    using TrendyMemes.Data.Common.Repositories;
    using TrendyMemes.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task VoteOnPostAsync(int postId, string userId, int value)
        {
            var vote = new Vote
            {
                PostId = postId,
                UserId = userId,
                Value = value,
            };

            await this.votesRepository.AddAsync(vote);
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
