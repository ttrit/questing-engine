using QuestingEngine.Model;
using System;

namespace QuestingEngine.Service.Commands
{
    public class UpdatePlayerPointEventArgs : EventArgs
    {
        public UpdatePlayerPointEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }
}
