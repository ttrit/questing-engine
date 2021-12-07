using QuestingEngine.Repository;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IPlayerService
    {
        Task GetCurrentQuestStatus(string playerId);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IQuestRepository _questRepository;

        public PlayerService(IPlayerRepository playerRepository,
            IMilestoneRepository milestoneRepository,
            IQuestRepository questRepository)
        {
            _playerRepository = playerRepository;
            _milestoneRepository = milestoneRepository;
            _questRepository = questRepository;
        }

        public async Task GetCurrentQuestStatus(string playerId)
        {
            var player = await _playerRepository.GetAsync(playerId);
            var currentQuest = await _questRepository.GetAsync(player.CurrentQuest.ToString());


        }
    }
}
