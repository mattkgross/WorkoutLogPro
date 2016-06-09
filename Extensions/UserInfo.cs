using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WorkoutLogPro.Areas.Teams.Models;

namespace WorkoutLogPro.Extensions
{
    public class AppUser : IdentityUser
    {
        public virtual UserInfo UserInfo { get; set; }
    }

    public class UserInfo : DbAccessibleModel
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public virtual ICollection<Team> Teams { get; set; }

        public UserInfo(string id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetFullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }

    public class AppUserContext : IdentityDbContext<AppUser>, IUpdateableDbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public AppUserContext()
            : base("DefaultConnection")
        {
        }

        public DbSet GetDataSet(Type type)
        {
            // Don't need type in this case since there's only one data set.
            return UserInfo;
        }
    }
}
