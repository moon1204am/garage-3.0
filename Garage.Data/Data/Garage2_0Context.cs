using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage.Domain.Entities;

namespace Garage.Data.Data
{
    public class Garage2_0Context : DbContext
    {
        public Garage2_0Context(DbContextOptions<Garage2_0Context> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; } = default!;
    }
}
