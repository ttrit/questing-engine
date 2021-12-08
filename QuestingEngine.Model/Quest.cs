using System.Collections.Generic;

namespace QuestingEngine.Model
{
    public class Quest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Milestone> Milestones { get; set; }
        public int TotalPointToComplete { get; set; }
    }
}
