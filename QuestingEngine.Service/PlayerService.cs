using QuestingEngine.Model;
using QuestingEngine.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IPlayerService
    {
        Task Create(Player player);
        Task<int> GetCurrentQuestStatus(string playerId);
        Task<IEnumerable<Milestone>> GetCompletedMilestones(string playerId);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IQuestRepository _questRepository;

        public PlayerService(IPlayerRepository playerRepository,
            IMilestoneRepository milestoneRepository,
            IQuestRepository questRepository)
        {
            _playerRepository = playerRepository;
            _questRepository = questRepository;
        }

        public async Task<int> GetCurrentQuestStatus(string playerId)
        {
            var player = await _playerRepository.GetAsync(playerId);
            var currentQuest = await _questRepository.GetAsync(player.CurrentQuest.Id.ToString());

            return (int)(((float)player.TotalPoint / currentQuest.TotalPointToComplete) * 100);
        }

        public async Task<IEnumerable<Milestone>> GetCompletedMilestones(string playerId)
        {
            var player = await _playerRepository.GetAsync(playerId);
            return player.CompletedMilestones;
        }

        public async Task Create(Player player)
        {
            await _playerRepository.Create(player);
        }
    }
}
