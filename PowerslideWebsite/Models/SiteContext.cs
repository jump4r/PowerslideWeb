using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PowerslideWebsite.Models
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions<SiteContext> options) : base(options)
        {

        }

        public DbSet<Submission> Submission { get; set; }
        public DbSet<Beatmap> Beatmap { get; set; }
    }
}
