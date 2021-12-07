using QuestingEngine.Repository;
using QuestingEngine.Repository.DbModels;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IBetRateService
    {
        Task Create(BetRate betRate);
        Task<BetRate> GetAsync(int chipAmountBet);
    }

    public class BetRateService : IBetRateService
    {
        private readonly IBetRateRepository _betRateRepository;

        public BetRateService(IBetRateRepository betRateRepository)
        {
            _betRateRepository = betRateRepository;
        }

        public async Task Create(BetRate betRate)
        {
            await _betRateRepository.CreateAsync(betRate);
        }

        public async Task<BetRate> GetAsync(int chipAmountBet)
        {
            return await _betRateRepository.GetAsync(chipAmountBet);
        }
    }
}
