using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilarTestTask.Model
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Male { get; set; }
        public int DepatmentId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int MandatoryInformationId { get; set; }
        public int IsAdmin { get; set; }

    }
}
