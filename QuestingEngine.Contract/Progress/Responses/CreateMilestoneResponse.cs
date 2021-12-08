using QuestingEngine.Model;
using System.Collections.Generic;

namespace QuestingEngine.Contract.Progress.Responses
{
    public class CreateMilestoneResponse
    {
        public int QuestPointsEarned { get; set; }
        public int TotalQuestPercentCompleted { get; set; }
        public IEnumerable<Milestone> MilestoneCompleted { get; set; }
    }
}
