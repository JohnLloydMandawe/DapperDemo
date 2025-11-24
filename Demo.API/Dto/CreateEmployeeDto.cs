using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Dto
{
    public class CreateEmployeeDto
    {
        public string fullname { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public int age { get; set; }
        public DateTime birthdate { get; set; } 

    }
}