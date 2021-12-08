using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Model;
using QuestingEngine.Service;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneController : ControllerBase
    {
        private readonly IMilestoneService _milestoneService;

        public MilestoneController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }

        [HttpPost]
        public async Task Create(Milestone milestone)
        {
            await _milestoneService.Create(milestone);
        }
    }
}
