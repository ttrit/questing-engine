using MongoDB.Bson.Serialization.Attributes;

namespace QuestingEngine.Repository.DbModels
{
    public class Milestone
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public int PointToComplete { get; set; }
        public int ChipsAwarded { get; set; }
    }
}
