using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogPro.Areas.Teams.Models;

namespace WorkoutLogPro.Extensions
{
    public static class ModelDbContextFactory
    {
        /// <summary>
        /// Maps a DbAccessibleModel to its corresponding IUpdateableDbContext.
        /// </summary>
        private static readonly Dictionary<Type, Type> map = new Dictionary<Type, Type>()
        {
            // AppUserContext db context.
            { typeof(UserInfo), typeof(AppUserContext) },
            // Context db context.
            { typeof(Team), typeof(Context) }
        };

        public static DbContext FactoryGetUpdateableDbContext(Type modelType)
        {
            if(map.ContainsKey(modelType))
            {
                if (map[modelType].IsAssignableFrom(typeof(IUpdateableDbContext)))
                {
                    return (DbContext)Activator.CreateInstance(map[modelType]);
                }
                else
                {
                    throw new Exception(string.Format("The specified value ({0}) for your key ({1}) was does not implement IUpdateableDbContext.",
                        map[modelType].ToString(), modelType.ToString()));
                }
            }
            throw new Exception(string.Format("Cannot find the specified UpdateableDbContext type ({0}).", modelType.ToString()));
        }
    }
}
