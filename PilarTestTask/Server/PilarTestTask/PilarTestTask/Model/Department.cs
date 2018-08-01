using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilarTestTask.Model
{
    public class Depatment 
    {
        public Depatment()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public int MandatoryInformationId { get; set; }

    }
}