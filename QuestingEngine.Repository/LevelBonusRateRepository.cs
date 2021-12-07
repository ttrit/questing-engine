using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface ILevelBonusRateRepository
    {
        Task<List<LevelBonusRate>> GetAsync();
        Task<LevelBonusRate?> GetAsync(string id);
        Task<LevelBonusRate?> GetAsync(int playerLevel);
        Task CreateAsync(LevelBonusRate levelBonusRate);
        Task UpdateAsync(string id, LevelBonusRate updatedBonusRate);
        Task RemoveAsync(string id);
    }

    public class LevelBonusRateRepository : ILevelBonusRateRepository
    {
        private readonly IMongoCollection<LevelBonusRate> _collection;

        public LevelBonusRateRepository(IOptions<MongoConfiguration> configuration)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _collection = mongoDb.GetCollection<LevelBonusRate>("LevelBonusRate");
        }

        public async Task<List<LevelBonusRate>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<LevelBonusRate?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(LevelBonusRate levelBonusRate) =>
            await _collection.InsertOneAsync(levelBonusRate);

        public async Task UpdateAsync(string id, LevelBonusRate updatedBonusRate) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedBonusRate);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);

        public async Task<LevelBonusRate> GetAsync(int playerLevel) =>
            await _collection.Find(x => x.Level == playerLevel).FirstOrDefaultAsync();
    }
}
