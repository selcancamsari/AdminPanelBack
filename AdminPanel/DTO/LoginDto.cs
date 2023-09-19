using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
