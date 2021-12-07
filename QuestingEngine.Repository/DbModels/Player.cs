using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace QuestingEngine.Repository.DbModels
{
    public class Player
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public ObjectId CurrentQuest { get; set; }
        public int TotalPoint { get; set; }
        public IEnumerable<ObjectId> CompletedMilestone { get; set; }
    }
}
