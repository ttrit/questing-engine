using QuestingEngine.Model;
using QuestingEngine.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface ISeedDataService
    {
        Task InitializeData();
    }

    public class SeedDataService : ISeedDataService
    {
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IQuestRepository _questRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IBetRateRepository _betRateRepository;
        private readonly ILevelBonusRateRepository _levelBonusRateRepository;

        public SeedDataService(
            IMilestoneRepository milestoneRepository,
            IQuestRepository questRepository,
            IPlayerRepository playerRepository,
            IBetRateRepository betRateRepository,
            ILevelBonusRateRepository levelBonusRateRepository)
        {
            _milestoneRepository = milestoneRepository;
            _questRepository = questRepository;
            _playerRepository = playerRepository;
            _betRateRepository = betRateRepository;
            _levelBonusRateRepository = levelBonusRateRepository;
        }

        public async Task InitializeData()
        {
            var milestone1Id = string.Empty;
            var milestone2Id = string.Empty;
            var milestone3Id = string.Empty;
            var questId = string.Empty;

            var existedMilestone = await _milestoneRepository.GetAsync();
            var existedQuests = await _questRepository.GetAsync();
            var existedPlayers = await _playerRepository.GetAsync();
            var existedBetRate = await _betRateRepository.GetAsync();
            var existedLevelBonusRate = await _levelBonusRateRepository.GetAsync();

            if (existedMilestone.Any() || existedQuests.Any() || existedPlayers.Any() || existedBetRate.Any() || existedLevelBonusRate.Any())
            {
                return;
            }

            var milestone1 = new Milestone
            {
                Index = "milestone1",
                Name = "milestone1",
                PointToComplete = 30,
                ChipsAwarded = 10
            };
            var milestone2 = new Milestone
            {
                Index = "milestone2",
                Name = "milestone2",
                PointToComplete = 50,
                ChipsAwarded = 15
            };
            var milestone3 = new Milestone
            {
                Index = "milestone3",
                Name = "milestone3",
                PointToComplete = 100,
                ChipsAwarded = 30
            };
            milestone1Id = await _milestoneRepository.Create(milestone1);
            milestone2Id = await _milestoneRepository.Create(milestone2);
            milestone3Id = await _milestoneRepository.Create(milestone3);


            var quest = new Quest
            {
                Name = "Quest1",
                Milestones = new List<Milestone>()
                    {
                        new Milestone(){ Id = milestone1Id },
                        new Milestone(){ Id = milestone2Id },
                        new Milestone(){ Id = milestone3Id }
                    },
                TotalPointToComplete = 50
            };
            questId = await _questRepository.Create(quest);



            var player = new Player
            {
                Name = "player one",
                Level = 2,
                CurrentQuest = new Quest { Id = questId },
                TotalPoint = 20,
                CompletedMilestones = new List<Milestone> { new Milestone() { Id = milestone1Id } }
            };
            await _playerRepository.Create(player);

            var betRate1 = new Repository.DbModels.BetRate
            {
                UpperBound = 10,
                LowerBound = 1,
                Rate = 0.5f
            };
            var betRate2 = new Repository.DbModels.BetRate
            {
                UpperBound = 20,
                LowerBound = 11,
                Rate = 0.6f
            };
            var betRate3 = new Repository.DbModels.BetRate
            {
                UpperBound = 30,
                LowerBound = 21,
                Rate = 0.8f
            };
            await _betRateRepository.CreateAsync(betRate1);
            await _betRateRepository.CreateAsync(betRate2);
            await _betRateRepository.CreateAsync(betRate3);

            var levelBonusRate1 = new Repository.DbModels.LevelBonusRate
            {
                Level = 1,
                BonusRate = 3
            };
            var levelBonusRate2 = new Repository.DbModels.LevelBonusRate
            {
                Level = 2,
                BonusRate = 4
            };
            var levelBonusRate3 = new Repository.DbModels.LevelBonusRate
            {
                Level = 3,
                BonusRate = 5
            };
            await _levelBonusRateRepository.CreateAsync(levelBonusRate1);
            await _levelBonusRateRepository.CreateAsync(levelBonusRate2);
            await _levelBonusRateRepository.CreateAsync(levelBonusRate3);
        }
    }
}
