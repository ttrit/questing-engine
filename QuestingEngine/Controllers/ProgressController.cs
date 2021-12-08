using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Contract.Progress.Requests;
using QuestingEngine.Contract.Progress.Responses;
using QuestingEngine.Service;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IQuestService _questingService;
        private readonly IPlayerService _playerService;
        private readonly IMediator _mediator;

        public ProgressController(
            IQuestService questingService,
            IPlayerService playerService,
            IMediator mediator)
        {
            _questingService = questingService ?? throw new ArgumentNullException(nameof(questingService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<CreateMilestoneResponse> CreateMilestone(CreateMilestoneRequest request)
        {
            var response = new CreateMilestoneResponse();

            response.QuestPointsEarned = await _questingService.UpdateQuestPointEarned(request.PlayerId, request.PlayerLevel, request.ChipAmountBet);
            response.TotalQuestPercentCompleted = await _playerService.GetCurrentQuestStatus(request.PlayerId);
            response.MilestoneCompleted = await _playerService.GetCompletedMilestones(request.PlayerId);

            return response;
        }
    }
}
