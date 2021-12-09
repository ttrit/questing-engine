using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using QuestingEngine.Models;
using QuestingEngine.Repository.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IMilestoneRepository
    {
        Task<string> Create(Model.Milestone milestone);
        Task<Model.Milestone> GetAsync(string id);
        Task<List<Model.Milestone>> GetAsync();
    }

    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly IMongoCollection<Milestone> _collection;
        private readonly IMapper _mapper;

        public MilestoneRepository(
            IOptions<MongoConfiguration> configuration,
            IMapper mapper)
        {
            _mapper = mapper;
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            _collection = mongoDb.GetCollection<Milestone>("Milestone");
        }

        public async Task<string> Create(Model.Milestone milestone) 
        {
            var milestoneDb = _mapper.Map<Milestone>(milestone);
            milestoneDb.Id = ObjectId.GenerateNewId().ToString();
            await _collection.InsertOneAsync(milestoneDb);
            return milestoneDb.Id;
        }
            
        public async Task<Model.Milestone> GetAsync(string id)
        {
            return _mapper.Map<Model.Milestone>(await _collection.Find(x => x.Id == id).FirstOrDefaultAsync());
        }

        public async Task<List<Model.Milestone>> GetAsync()
        {
            return _mapper.Map<List<Model.Milestone>>(await _collection.Find(_ => true).ToListAsync());
        }
    }
}
