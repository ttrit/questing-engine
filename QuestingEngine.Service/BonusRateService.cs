using QuestingEngine.Repository;
using QuestingEngine.Repository.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IBonusRateService
    {
        Task Create(LevelBonusRate levelBonusRate);
        Task<List<LevelBonusRate>> GetAsync();
        Task<LevelBonusRate> GetAsync(string id);
        Task<LevelBonusRate> GetAsync(int playerLevel);
    }

    public class BonusRateService : IBonusRateService
    {
        private readonly ILevelBonusRateRepository _bonusRateRepository;

        public BonusRateService(ILevelBonusRateRepository bonusRateRepository)
        {
            _bonusRateRepository = bonusRateRepository;
        }

        public async Task Create(LevelBonusRate levelBonusRate)
        {
            await _bonusRateRepository.CreateAsync(levelBonusRate);
        }

        public async Task<List<LevelBonusRate>> GetAsync()
        {
            return await _bonusRateRepository.GetAsync();
        }

        public async Task<LevelBonusRate> GetAsync(string id)
        {
            return await _bonusRateRepository.GetAsync(id);
        }

        public async Task<LevelBonusRate> GetAsync(int playerLevel)
        {
            return await _bonusRateRepository.GetAsync(playerLevel);
        }
    }
}
