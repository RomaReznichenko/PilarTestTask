using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PilarTestTask.Model
{
    public class DataBaceContext : DbContext
    {
        public DataBaceContext(DbContextOptions<DataBaceContext> options)
            : base(options)
        {
        }

        public DbSet<MandatoryInformation> MandatoryInformation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Depatment> Depatment { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<Business> Business { get; set; }

    }
}
