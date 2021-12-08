using MediatR;
using QuestingEngine.Model;
using QuestingEngine.Repository;
using QuestingEngine.Service.Commands;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IQuestService
    {
        Task Create(Quest quest);

        Task<int> UpdateQuestPointEarned(string playerId, int playerLevel, int chipAmountBet);
    }

    public class QuestService : IQuestService
    {
        private readonly IQuestRepository _questRepository;
        private readonly ILevelBonusRateRepository _bonusRateRepository;
        private readonly IBetRateRepository _betRateRepository;
        private readonly IMediator _mediator;

        public QuestService(
            IQuestRepository questRepository,
            ILevelBonusRateRepository bonusRateRepository, 
            IBetRateRepository betRateRepository,
            IMediator mediator)
        {
            _questRepository = questRepository ?? throw new ArgumentNullException(nameof(questRepository));
            _bonusRateRepository = bonusRateRepository ?? throw new ArgumentNullException(nameof(bonusRateRepository));
            _betRateRepository = betRateRepository ?? throw new ArgumentNullException(nameof(betRateRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Create(Quest quest)
        {
            await _questRepository.Create(quest);
        }

        public async Task<int> UpdateQuestPointEarned(string playerId, int playerLevel, int chipAmountBet)
        {
            var levelBonusRate = await _bonusRateRepository.GetAsync(playerLevel);
            var rateFromBet = await _betRateRepository.GetAsync(chipAmountBet);

            var chipAmountEarned = Math.Floor((chipAmountBet * rateFromBet.Rate) + (playerLevel * levelBonusRate.BonusRate));

            await _mediator.Send(new UpdatePlayerPointCommand
            {
                Id = playerId,
                UpdatePoint = (int)chipAmountEarned
            });

            return (int)chipAmountEarned;
        }
    }
}
