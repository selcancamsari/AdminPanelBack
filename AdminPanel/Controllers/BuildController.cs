using AdminPanel.DTO;
using AdminPanel.Models;
using AdminPanel.Services;
using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        public BuildController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpPost]
        [Route("BuildingAdd")]
        public async Task<IActionResult> BuildingAdd([FromBody] BuildDto request)
        {
            Building building = new Building()
            {
                BuildingCost = request.BuildingCost,
                BuildingType = request.BuildingType,
                ConstructionTime = request.ConstructionTime
            };

            await _mongoDBService.CreateAsync(building);

            //burada building typeların statuslarını false yapyoruz
            await BuildingTypeUpdate(request.BuildingType);

            return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);

        }

        [HttpGet]
        [Route("GetBuilding")]
        public async Task<List<Building>> GetBuilding()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpGet]
        [Route("GetBuildingTypeList")]
        public async Task<List<BuildingTypes>> GetBuildingTypeList()
        {
            return await _mongoDBService.GetBuildingTypeAsync();
        }

        [NonAction]
        [HttpPut("{id}")]
        [Route("BuildingUpdate")]
        public async Task<IActionResult> AddToBuildings(string id, [FromBody] string buildingId)
        {
            await _mongoDBService.AddToBuildingsAsync(id, buildingId);
            return NoContent();
        }

        [NonAction]
        [HttpDelete("{id}")]
        [Route("BuildingDelete")]
        public async Task<IActionResult> DeleteBuilding(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

        [NonAction]
        [HttpPut("{id}")]
        [Route("BuildingTypeUpdate")]
        public async Task<IActionResult> BuildingTypeUpdate(int buildingType)
        {
            await _mongoDBService.BuildingTypeUpdate(buildingType);
            return NoContent();
        }
    }
}
