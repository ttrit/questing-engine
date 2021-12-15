using QuestingEngine.Repository;
using QuestingEngine.Service.Commands;
using System.Linq;

namespace QuestingEngine.Service
{
    public class AchievementObserver
    {
        private readonly IMilestoneRepository _milestoneRepository;

        public AchievementObserver(PlayerObservable playerObservable,
            IMilestoneRepository milestoneRepository)
        {
            playerObservable.UpdatePlayerPointEvent += PlayerPointChange;
            _milestoneRepository = milestoneRepository;
        }

        private void PlayerPointChange(object sender, UpdatePlayerPointEventArgs e)
        {
            var player = e.Player;

            var milestones = _milestoneRepository.GetAsync().Result;
            var milestoneCanAchieve = milestones
                .Where(m => m.PointToComplete <= player.TotalPoint)
                .OrderByDescending(m => m.PointToComplete)
                .First();
            if (!player.CompletedMilestones.Exists(m => m.Id == milestoneCanAchieve.Id))
            {
                player.TotalPoint += milestoneCanAchieve.ChipsAwarded;
                player.CompletedMilestones.Add(milestoneCanAchieve);
                // TODO: save player to database
            }

            System.Console.WriteLine(player.TotalPoint);
        }
    }
}
