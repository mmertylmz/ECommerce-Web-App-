using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
    {
        //If we want to use DotNet CLI, We need to configure this setup, if not CLI will throw an DesignTime error.
        public ECommerceAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString); //appsettings.json
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
