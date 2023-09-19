using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public List<string> CollectionName { get; set; } = null!;
    }
}
