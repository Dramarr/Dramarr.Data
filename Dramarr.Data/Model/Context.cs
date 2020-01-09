using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Dramarr.Data.Model
{
    class Context : DbContext
    {
        public Context(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}
