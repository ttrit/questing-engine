using QuestingEngine.Repository;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IQuestingService
    {
        Task<int> CalculateQuestPointEarned(int chipAmountBet, int playerLevel);
    }

    public class QuestingService : IQuestingService
    {
        private readonly ILevelBonusRateRepository _bonusRateRepository;
        private readonly IBetRateRepository _betRateRepository;

        public QuestingService(ILevelBonusRateRepository bonusRateRepository, IBetRateRepository betRateRepository)
        {
            _bonusRateRepository = bonusRateRepository;
            _betRateRepository = betRateRepository;
        }

        public async Task<int> CalculateQuestPointEarned(int chipAmountBet, int playerLevel)
        {
            var levelBonusRate = await _bonusRateRepository.GetAsync(playerLevel);
            var rateFromBet = await _betRateRepository.GetAsync(chipAmountBet);

            var chipAmountEarned = (chipAmountBet * rateFromBet.Rate) + (playerLevel * levelBonusRate.BonusRate);

            return (int)Math.Floor(chipAmountEarned);
        }
    }
}
