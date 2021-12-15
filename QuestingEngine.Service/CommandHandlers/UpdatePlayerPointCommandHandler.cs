using MediatR;
using QuestingEngine.Repository;
using QuestingEngine.Service.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuestingEngine.Service.CommandHandlers
{
    public class UpdatePlayerPointCommandHandler : IRequestHandler<UpdatePlayerPointCommand, bool>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMilestoneRepository _milestoneRepository;

        public UpdatePlayerPointCommandHandler(
            IPlayerRepository playerRepository,
            IMilestoneRepository milestoneRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _milestoneRepository = milestoneRepository ?? throw new ArgumentNullException(nameof(milestoneRepository));
        }

        public async Task<bool> Handle(UpdatePlayerPointCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetAsync(request.Id);

            player.TotalPoint += request.UpdatePoint;

            await _playerRepository.UpdateAsync(player);

            var playerObservable = new PlayerObservable();
            var achievementObserver = new AchievementObserver(playerObservable, _milestoneRepository);
            playerObservable.Player = player;

            return true;
        }
    }
}
