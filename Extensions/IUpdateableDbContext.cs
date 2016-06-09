using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogPro.Extensions
{
    /// <summary>
    /// Ensures that an Updatable DbContext can return the correct data set for an update.
    /// </summary>
    public interface IUpdateableDbContext
    {
        /// <summary>
        /// Returns the proper DataSet to update for an update call.
        /// </summary>
        /// <param name="type">The object type of the DbSet to fetch from this DbContext.</param>
        /// <returns></returns>
        DbSet GetDataSet(Type type);
    }
}
