using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogPro.Extensions
{
    public abstract class UpdateableDbContext : DbContext
    {
        public UpdateableDbContext()
            : base("DefaultConnection")
        {
        }

        public abstract DbSet GetDataSet();
    }
}
