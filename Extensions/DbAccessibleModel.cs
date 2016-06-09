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
        protected UpdateableDbContext DbContext { get; private set; }

        /// <summary>
        /// Updates the object in the DB.
        /// </summary>
        public virtual void UpdateDb()
        {
            SetDbContext();

            GetDbSet().Attach(this);
            DbContext.Entry(this).State = EntityState.Modified;
            DbContext.SaveChanges();

            CloseDb();
        }

        /// <summary>
        /// Returns a UpdateableDbContext for this model type. It is the responsibility of the caller to dispose.
        /// </summary>
        /// <returns></returns>
        protected UpdateableDbContext GetDbContext()
        {
            object inst = Activator.CreateInstance(this.GetType());
            if(inst.GetType().IsSubclassOf(typeof(UpdateableDbContext)))
            {
                return (UpdateableDbContext)inst;
            }

            throw new Exception(string.Format("Could not evaluate an UpdateableDbContext for the {0} model.", inst.GetType().ToString()));
        }

        /// <summary>
        /// Returns the DbSet for this model's DbContext.
        /// </summary>
        /// <returns></returns>
        private DbSet GetDbSet()
        {
            if(DbContext == null)
            {
                SetDbContext();
            }

            return DbContext.GetDataSet();
        }

        /// <summary>
        /// Refreshes the dbContext to a new session.
        /// </summary>
        private void SetDbContext()
        {
            CloseDb();
            DbContext = GetDbContext();
        }

        /// <summary>
        /// Releases resources after a SetDbContext call.
        /// </summary>
        private void CloseDb()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
                DbContext = null;
            }
        }
    }
}
