using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuestingEngine.Repository.DbModels
{
    public class LevelBonusRate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int Level { get; set; }
        public float BonusRate { get; set; }
    }
}
