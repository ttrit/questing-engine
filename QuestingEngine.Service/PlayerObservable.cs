using QuestingEngine.Model;
using QuestingEngine.Service.Commands;
using System;

namespace QuestingEngine.Service
{
    public class PlayerObservable
    {
        private Player player;

        public event EventHandler<UpdatePlayerPointEventArgs> UpdatePlayerPointEvent = delegate { };

        public Player Player
        {
            get => player;
            set
            {
                player = value;
                UpdatePlayerPointEvent(this, new UpdatePlayerPointEventArgs(player));
            }
        }
    }
}
