using Microsoft.AspNetCore.Mvc;
using QuestingEngine.Contract.Progress.Requests;
using QuestingEngine.Contract.Progress.Responses;
using System;
using System.Threading.Tasks;

namespace QuestingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        [HttpPost]
        public async Task<CreateMilestoneResponse> CreateMilestone(CreateMilestoneRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
