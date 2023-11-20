using Garage.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Garage.Data.Migrations
{
    internal class Garage2_0ContextFactory : IDesignTimeDbContextFactory<Garage2_0Context>
    {
        public Garage2_0Context CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<Garage2_0Context>();
            options.UseSqlServer("Not used for scaffolding"); //Change to actual connctionstring

            return new Garage2_0Context(options.Options);
        }
    }

    
}
