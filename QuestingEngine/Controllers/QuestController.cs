using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Model;
using QuestingEngine.Service;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpPost]
        public async Task Create(Quest quest)
        {
            await _questService.Create(quest);
        }
    }
}
