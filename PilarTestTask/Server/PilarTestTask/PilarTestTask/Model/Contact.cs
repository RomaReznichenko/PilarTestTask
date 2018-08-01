using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilarTestTask.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int MandatoryInformationId { get; set; }
        public int UserId { get; set; }
    }
}
