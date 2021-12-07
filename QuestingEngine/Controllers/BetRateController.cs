using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Repository.DbModels;
using QuestingEngine.Service;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetRateController : ControllerBase
    {
        private readonly IBetRateService _betRateService;

        public BetRateController(IBetRateService betRateService)
        {
            _betRateService = betRateService;
        }

        [HttpPost]
        public async Task Create(BetRate betRate)
        {
            await _betRateService.Create(betRate);
        }

        [HttpGet]
        public async Task<BetRate> GetAsync(int chipAmount)
        {
            return await _betRateService.GetAsync(chipAmount);
        }
    }
}
