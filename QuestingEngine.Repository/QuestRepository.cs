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
    public interface IQuestRepository
    {
        Task Create(Model.Quest quest);
        Task<Model.Quest> GetAsync(string id);
    }

    public class QuestRepository : IQuestRepository
    {
        private readonly IMongoCollection<Quest> _questCollection;
        private readonly IMapper _mapper;

        public QuestRepository(IOptions<MongoConfiguration> configuration,
            IMapper mapper)
        {
            _mapper = mapper;
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _questCollection = mongoDb.GetCollection<Quest>("Quest");
        }

        public async Task Create(Model.Quest quest)
        {
            var questDb = _mapper.Map<Quest>(quest);
            questDb.Id = ObjectId.GenerateNewId().ToString();
            var milestones = _mapper.Map<IEnumerable<Milestone>>(quest.Milestones);
            questDb.Milestones = milestones.Select(m => new ObjectId(m.Id)).ToList();

            await _questCollection.InsertOneAsync(questDb);
        }
            
        public async Task<Model.Quest> GetAsync(string id)
        {
            return _mapper.Map<Model.Quest>(await _questCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
        }
    }
}
