using Microsoft.EntityFrameworkCore;
using Persistence.Context.Extensions.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.SeedData();
        }
    }
}
