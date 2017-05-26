using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }
        public virtual DbSet<ShortUrl> ShortUrls { get; set; }
        public virtual DbSet<Stat> Stats { get; set; }
    }
}
