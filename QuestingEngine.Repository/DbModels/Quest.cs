using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace QuestingEngine.Repository.DbModels
{
    public class Quest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ObjectId> Milestones { get; set; }
        public int TotalPointToComplete { get; set; }
    }
}
