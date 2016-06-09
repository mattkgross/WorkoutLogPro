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
        protected DbContext DbContext { get; private set; }

        public virtual void InsertDb()
        {
            SetDbContext();

            GetDbSet().Add(this);
            DbContext.SaveChanges();

            CloseDb();
        }

        /// <summary>
        /// Updates the object in the DB.
        /// </summary>
        public virtual void UpdateDb()
        {
            SetDbContext();

            if (GetDbSet().Find(this) != null)
            {
                GetDbSet().Attach(this);
                DbContext.Entry(this).State = EntityState.Modified;
                DbContext.SaveChanges();

                CloseDb();
            }
            else
            {
                CloseDb();
                InsertDb();
            }
        }

        public virtual void RemoveDb()
        {
            SetDbContext();

            GetDbSet().Remove(this);
            DbContext.SaveChanges();

            CloseDb();
        }

        /// <summary>
        /// Returns a UpdateableDbContext for this model type. It is the responsibility of the caller to dispose.
        /// </summary>
        /// <returns></returns>
        protected DbContext GetDbContext()
        {
            return ModelDbContextFactory.FactoryGetUpdateableDbContext(this.GetType());
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

            // Factory ensures that DBContext is always castable to this.
            return ((IUpdateableDbContext)DbContext).GetDataSet(this.GetType());
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
