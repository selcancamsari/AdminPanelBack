using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.DTO
{
    public class BuildDto
    {
        [Required]
        public int BuildingType { get; set; }

        [Required]
        [Range(1,double.MaxValue)]
        public decimal BuildingCost { get; set; }

        [Required]
        [Range(30,1800)]
        public int ConstructionTime { get; set; }
    }
}
