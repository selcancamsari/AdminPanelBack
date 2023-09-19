using AdminPanel.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Building> _buildingsCollection;
        private readonly IMongoCollection<BuildingTypes> _buildingTypesCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _buildingsCollection = database.GetCollection<Building>(mongoDBSettings.Value.CollectionName[0]);
            _buildingTypesCollection = database.GetCollection<BuildingTypes>(mongoDBSettings.Value.CollectionName[1]);
        }

        public async Task<List<Building>> GetAsync()
        {
            return await _buildingsCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(Building building)
        {
            await _buildingsCollection.InsertOneAsync(building);
            return;
        }
        public async Task AddToBuildingsAsync(string id, string buildingId)
        {
            FilterDefinition<Building> filter = Builders<Building>.Filter.Eq("Id", id);
            UpdateDefinition<Building> update = Builders<Building>.Update.AddToSet<string>("buildingIds", buildingId);
            await _buildingsCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Building> filter = Builders<Building>.Filter.Eq("Id", id);
            await _buildingsCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task<List<BuildingTypes>> GetBuildingTypeAsync()
        {
            FilterDefinition<BuildingTypes> filter = Builders<BuildingTypes>.Filter.Eq("Status", 1);

            return await _buildingTypesCollection.Find(filter).ToListAsync();
        }

        public async Task BuildingTypeUpdate(int buildingType)
        {
            FilterDefinition<BuildingTypes> filter = Builders<BuildingTypes>.Filter.Eq("BuildingType", buildingType);
            UpdateDefinition<BuildingTypes> update = Builders<BuildingTypes>.Update.Set("Status", 0);
            await _buildingTypesCollection.UpdateOneAsync(filter, update);
            return;
        }
    }
}
