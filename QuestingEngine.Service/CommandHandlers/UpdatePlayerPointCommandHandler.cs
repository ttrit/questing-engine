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

        public UpdatePlayerPointCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<bool> Handle(UpdatePlayerPointCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetAsync(request.Id);

            player.TotalPoint += request.UpdatePoint;

            await _playerRepository.Update(player);

            return true;
        }
    }
}
