using QuestingEngine.Model;
using QuestingEngine.Repository;
using System.Threading.Tasks;

namespace QuestingEngine.Service
{
    public interface IMilestoneService
    {
        Task Create(Milestone milestone);
    }

    public class MilestoneService : IMilestoneService
    {
        private readonly IMilestoneRepository _milestoneRepository;

        public MilestoneService(IMilestoneRepository milestoneRepository)
        {
            _milestoneRepository = milestoneRepository;
        }

        public async Task Create(Milestone milestone)
        {
            await _milestoneRepository.Create(milestone);
        }
    }
}
