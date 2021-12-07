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
        private readonly IQuestingService _questingService;

        public ProgressController(IQuestingService questingService)
        {
            _questingService = questingService;
        }

        [HttpPost]
        public async Task<CreateMilestoneResponse> CreateMilestone(CreateMilestoneRequest request)
        {
            var response = new CreateMilestoneResponse();

            response.QuestPointsEarned = await _questingService.CalculateQuestPointEarned(request.ChipAmountBet, request.PlayerLevel);


        }
    }
}
