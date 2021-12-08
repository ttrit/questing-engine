using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Model;
using QuestingEngine.Service;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task Create(Player player)
        {
            await _playerService.Create(player);
        }
    }
}
