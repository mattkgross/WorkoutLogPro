using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogPro.Areas.Teams.Models;

namespace WorkoutLogPro.Extensions
{
    class Context : DbContext, IUpdateableDbContext
    {
        public DbSet<Team> Teams { get; set; }
        // Posts will also go here.

        public Context()
            : base("DefaultConnection")
        {
        }

        public DbSet GetDataSet(Type type)
        {
            if (type.Equals(typeof(Team)))
            {
                return Teams;
            }
            // else if type is a different var.

            throw new Exception(string.Format("No DataSet found for the {0} model type.", type.ToString()));
        }
    }
}
