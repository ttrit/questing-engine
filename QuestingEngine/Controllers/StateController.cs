using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Contract.State.Responses;
using QuestingEngine.Service;
using System.Linq;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public StateController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<GetPlayerStateResponse> GetPlayerState(string playerId)
        {
            var response = new GetPlayerStateResponse();
            response.TotalQuestPercentCompleted = await _playerService.GetCurrentQuestStatus(playerId);
            var completedMilestones = await _playerService.GetCompletedMilestones(playerId);
            response.LastMilestoneIndexCompleted = completedMilestones.Last().Id;

            return response;
        }
    }
}
