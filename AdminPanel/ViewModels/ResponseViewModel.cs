using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.ViewModels
{
    public class ResponseViewModel
    {
        public bool isSuccess { get; set; }
        public string Messages { get; set; }
        public string Token { get; set; }
    }
}
