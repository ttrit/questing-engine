namespace QuestingEngine.Contract.Progress.Requests
{
    public class CreateMilestoneRequest
    {
        public string PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }
    }
}
