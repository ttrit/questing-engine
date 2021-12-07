using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IPlayerRepository
    {
        Task Create(Player player);
        Task<Player> GetAsync(string id);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMongoCollection<Player> _collection;

        public PlayerRepository(IOptions<MongoConfiguration> configuration)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _collection = mongoDb.GetCollection<Player>("Player");
        }

        public async Task Create(Player player) =>
            await _collection.InsertOneAsync(player);

        public async Task<Player> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
