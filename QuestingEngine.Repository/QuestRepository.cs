using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IQuestRepository
    {
        Task Create(Quest quest);
        Task<Quest> GetAsync(string id);
    }

    public class QuestRepository : IQuestRepository
    {
        private readonly IMongoCollection<Quest> _collection;

        public QuestRepository(IOptions<MongoConfiguration> configuration)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _collection = mongoDb.GetCollection<Quest>("Quest");
        }

        public async Task Create(Quest quest) =>
            await _collection.InsertOneAsync(quest);

        public async Task<Quest> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
