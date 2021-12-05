using System.Collections.Generic;

namespace QuestingEngine.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public Quest CurrentQuest { get; set; }
        public int TotalPoint { get; set; }
        public IEnumerable<Milestone> CompletedMilestone { get; set; }
    }
}
