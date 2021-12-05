using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Contract.State.Responses;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        [HttpGet]
        public async Task<GetPlayerStateResponse> GetPlayerState(string playerId)
        {
            throw new NotImplementedException();
        }
    }
}
