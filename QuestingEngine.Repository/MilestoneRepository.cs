using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IMilestoneRepository
    {
        Task Create(Milestone milestone);
        Task<Milestone> GetAsync(string id);
    }

    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly IMongoCollection<Milestone> _collection;

        public MilestoneRepository(IOptions<MongoConfiguration> configuration)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _collection = mongoDb.GetCollection<Milestone>("Milestone");
        }

        public async Task Create(Milestone milestone) =>
            await _collection.InsertOneAsync(milestone);

        public async Task<Milestone> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
