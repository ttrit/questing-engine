using MediatR;

namespace QuestingEngine.Service.Commands
{
    public class UpdatePlayerPointCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public int UpdatePoint { get; set; }
    }
}
