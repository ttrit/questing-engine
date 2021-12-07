using MongoDB.Bson.Serialization.Attributes;

namespace QuestingEngine.Repository.DbModels
{
    public class BetRate
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public int UpperBound { get; set; }
        public int LowerBound { get; set; }
        public float Rate { get; set; }
    }
}
