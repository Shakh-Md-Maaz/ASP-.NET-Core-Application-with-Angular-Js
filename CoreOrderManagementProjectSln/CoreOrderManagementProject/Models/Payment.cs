using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CardNO { get; set; }
        public decimal Pin { get; set; }
    }
}
