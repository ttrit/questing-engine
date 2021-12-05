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
        public Task<int> CalculateQuestPointEarned(int chipAmountBet, int playerLevel)
        {
            throw new NotImplementedException();
        }
    }
}
