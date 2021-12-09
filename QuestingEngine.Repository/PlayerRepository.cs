using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IPlayerRepository
    {
        Task CreateAsync(Model.Player player);
        Task<Model.Player> GetAsync(string id);
        Task<List<Model.Player>> GetAsync();
        Task UpdateAsync(Model.Player player);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<Milestone> _milestoneCollection;
        private readonly IMongoCollection<Quest> _questCollection;
        private readonly IMapper _mapper;

        public PlayerRepository(
            IOptions<MongoConfiguration> configuration,
            IMapper mapper)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _playerCollection = mongoDb.GetCollection<Player>("Player");
            _milestoneCollection = mongoDb.GetCollection<Milestone>("Milestone");
            _questCollection = mongoDb.GetCollection<Quest>("Quest");

            _mapper = mapper;
        }

        public async Task CreateAsync(Model.Player player)
        {
            var playerDb = _mapper.Map<Player>(player);
            playerDb.Id = ObjectId.GenerateNewId().ToString();
            playerDb.CompletedMilestones = player.CompletedMilestones.Select(m => new ObjectId(m.Id));

            await _playerCollection.InsertOneAsync(playerDb);
        }

        public async Task<Model.Player> GetAsync(string id)
        {
            var playerDb = await _playerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            var player = new Model.Player
            {
                Name = playerDb.Name,
                Level = playerDb.Level,
                TotalPoint = playerDb.TotalPoint,
                CompletedMilestones = new List<Model.Milestone>()
            };

            foreach (var mileStoneId in playerDb.CompletedMilestones)
            {
                var milestone = (await _milestoneCollection.Find(m => m.Id == mileStoneId.ToString()).ToListAsync()).First();
                player.CompletedMilestones.Add(_mapper.Map<Model.Milestone>(milestone));
            }

            var questDb = (await _questCollection.Find(q => q.Id == playerDb.CurrentQuest.ToString()).ToListAsync()).First();
            player.CurrentQuest = _mapper.Map<Model.Quest>(questDb);

            return player;
        }

        public async Task<List<Model.Player>> GetAsync()
        {
            return _mapper.Map<List<Model.Player>>(await _playerCollection.Find(_ => true).ToListAsync());
        }

        public async Task UpdateAsync(Model.Player player)
        {
            await _playerCollection.ReplaceOneAsync(x => x.Id == player.Id, _mapper.Map<Player>(player));
        }
    }
}
