using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LimitsDBGenerator
{
    public class HashTableDocument
    {
        public ObjectId Id { get; set; }
        [BsonExtraElements]
        public Dictionary<string, object> Values { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Limits");
            var collection = db.GetCollection<HashTableDocument>("PolynomialRotios");
            for (int k = 0; k < 100000; ++k)
            {
                var document = new HashTableDocument
                {
                    Id = ObjectId.GenerateNewId(),
                    Values = new Dictionary<string, object>()
                };
                var numerator = new Polynom();
                var denominator = new Polynom();
                var rand = new Random();
                var numSize = rand.Next(1, 15);
                for (int i = 0; i < numSize; ++i)
                    numerator.Coefs.Add(rand.NextDouble() * Math.Pow(10, rand.Next(1, 6)));
                var numStr = "";
                foreach (var elem in numerator.Coefs)
                    numStr += elem.ToString() + " ";
                var denSize = rand.Next(1, 15);
                for (int i = 0; i < denSize; ++i)
                    denominator.Coefs.Add(rand.NextDouble() * Math.Pow(10, rand.Next(1, 6)));
                var denStr = "";
                foreach (var elem in denominator.Coefs)
                    denStr += elem.ToString() + " ";
                var rotio = $"{numStr}/ {denStr}";
                var valuesAndLimits = new Dictionary<string, object>();
                for (int j = 0; j < 100; ++j)
                {
                    var valueAt = rand.NextDouble() * Math.Pow(10, rand.Next(1, 22 / Math.Max(numSize, denSize)));
                    decimal.TryParse(LimitCalculator.CalculateLimit(numerator, denominator, valueAt.ToString()), out decimal limit);
                    valuesAndLimits.TryAdd(valueAt.ToString(), limit);
                }
                var bsonDoc = new BsonDocument(valuesAndLimits);
                document.Values.TryAdd(rotio, bsonDoc);
                collection.InsertOne(document);
            }
        }
    }
}
