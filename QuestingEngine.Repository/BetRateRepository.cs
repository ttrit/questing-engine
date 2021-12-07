using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IBetRateRepository
    {
        Task<List<BetRate>> GetAsync();
        Task<BetRate?> GetAsync(string id);
        Task<BetRate?> GetAsync(int chipAmount);
        Task CreateAsync(BetRate betRate);
        Task UpdateAsync(string id, BetRate updatedBetRate);
        Task RemoveAsync(string id);
    }

    public class BetRateRepository : IBetRateRepository
    {
        private readonly IMongoCollection<BetRate> _collection;

        public BetRateRepository(IOptions<MongoConfiguration> configuration)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _collection = mongoDb.GetCollection<BetRate>("BetRate");
        }

        public async Task<List<BetRate>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<BetRate?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BetRate betRate) =>
            await _collection.InsertOneAsync(betRate);

        public async Task UpdateAsync(string id, BetRate updatedBetRate) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedBetRate);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);

        public async Task<BetRate> GetAsync(int chipAmountBet) =>
            await _collection.Find(x => x.UpperBound > chipAmountBet && x.LowerBound < chipAmountBet).FirstOrDefaultAsync();
    }
}
