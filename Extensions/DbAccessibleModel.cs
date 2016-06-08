using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogPro.Extensions
{
    public abstract class DbAccessibleModel
    {
        protected UpdateableDbContext dbContext;

        /// <summary>
        /// Updates the object in the DB.
        /// </summary>
        public virtual void UpdateDb()
        {
            SetDbContext();

            GetDbSet().Attach(this);
            dbContext.Entry(this).State = EntityState.Modified;
            dbContext.SaveChanges();

            CloseDb();
        }

        /// <summary>
        /// Returns a UpdateableDbContext for this model type. It is the responsibility of the caller to dispose.
        /// </summary>
        /// <returns></returns>
        protected abstract UpdateableDbContext GetDbContext();

        /// <summary>
        /// Returns the DbSet for this model's DbContext.
        /// </summary>
        /// <returns></returns>
        private DbSet GetDbSet()
        {
            if(dbContext == null)
            {
                SetDbContext();
            }

            return dbContext.GetDataSet();
        }

        /// <summary>
        /// Refreshes the dbContext to a new session.
        /// </summary>
        private void SetDbContext()
        {
            dbContext = GetDbContext();
        }

        /// <summary>
        /// Releases resources after a SetDbContext call.
        /// </summary>
        private void CloseDb()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
                dbContext = null;
            }
        }
    }
}
