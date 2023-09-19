using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class BuildingTypes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int BuildingType { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
