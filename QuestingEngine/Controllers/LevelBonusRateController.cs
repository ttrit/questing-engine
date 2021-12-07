using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Repository.DbModels;
using QuestingEngine.Service;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelBonusRateController : ControllerBase
    {
        private readonly IBonusRateService _bonusRateService;

        public LevelBonusRateController(IBonusRateService bonusRateService)
        {
            _bonusRateService = bonusRateService;
        }

        [HttpPost]
        public async Task Create(LevelBonusRate levelBonusRate)
        {
            await _bonusRateService.Create(levelBonusRate);
        }

        [HttpGet]
        public async Task<LevelBonusRate> GetAsync(int playerLevel)
        {
            return await _bonusRateService.GetAsync(playerLevel);
        }
    }
}
