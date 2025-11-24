using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Dto
{
    public class UpdateJob
    {
        public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public decimal beginning_salary { get; set; }
    }
}