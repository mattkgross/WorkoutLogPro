using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogPro.Extensions;

namespace WorkoutLogPro.Areas.Teams.Models
{
    /// <summary>
    /// The DB Model for a Team.
    /// </summary>
    public class TeamContext : UpdateableDbContext
    {
        public DbSet<Team> Teams { get; set; }

        public TeamContext()
            : base()
        {
        }

        public override DbSet GetDataSet()
        {
            return Teams;
        }
    }

    /// <summary>
    /// The model for a Team.
    /// </summary>
    public class Team : DbAccessibleModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }
        private string EnrollmentKey { get; set; }

        public virtual ICollection<UserInfo> Members { get; private set; }
        public virtual ICollection<UserInfo> Admins { get; private set; }

        public bool MatchesEnrollmentKey(string key)
        {
            return PasswordHasher.HashPassword(key).Equals(EnrollmentKey);
        }

        public bool ChangeEnrollmentKey(string pastKey, string presentKey)
        {
            if (MatchesEnrollmentKey(pastKey))
            {
                EnrollmentKey = PasswordHasher.HashPassword(presentKey);
                UpdateDb();
                return true;
            }

            return false;
        }

        protected override UpdateableDbContext GetDbContext()
        {
            return new TeamContext();
        }
    }
}
