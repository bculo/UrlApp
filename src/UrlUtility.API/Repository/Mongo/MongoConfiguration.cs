using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using UrlUtility.API.Entities;

namespace UrlUtility.API.Repository.Mongo
{
    public static class MongoConfiguration
    {
        public static void InitializeMongoMapping()
        {
            BsonClassMap.RegisterClassMap<Url>(options =>
            {
                options.SetIgnoreExtraElements(true);
                options.MapMember(member => member.PageUrl);
                options.MapMember(member => member.CreatedOn);
                options.MapIdMember(member => member.UrlIdentifier).SetIdGenerator(StringObjectIdGenerator.Instance);
                options.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}
