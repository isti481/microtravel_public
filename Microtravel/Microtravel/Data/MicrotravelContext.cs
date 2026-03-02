using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microtravel.Models;

namespace Microtravel.Data
{
    public class MicrotravelContext : DbContext
    {
        public MicrotravelContext (DbContextOptions<MicrotravelContext> options)
            : base(options)
        {
        }

        public DbSet<Microtravel.Models.Travel> Travel { get; set; } = default!;
        //public DbSet<Microtravel.Models.Traveler> Traveler { get; set; } = default!;
        public DbSet<Microtravel.Models.TravelDealType> TravelDealType { get; set; } = default!;
        public DbSet<Microtravel.Models.TravelType> TravelType { get; set; } = default!;
        public DbSet<Microtravel.Models.ApiUser> Apiuser { get; set; } = default!;
    }
}
