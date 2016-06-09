using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogPro.Areas.Teams.Models;

namespace WorkoutLogPro.Extensions
{
    public static class ModelDbContextFactory
    {
        /// <summary>
        /// Maps a DbAccessibleModel to its corresponding UpdateableDbContext.
        /// </summary>
        private static readonly Dictionary<Type, Type> map = new Dictionary<Type, Type>()
        {
            { typeof(Team), typeof(TeamContext)}
        };

        public static UpdateableDbContext FactoryGetUpdateableDbContext(Type modelType)
        {
            if(map.ContainsKey(modelType))
            {
                if (map[modelType].IsSubclassOf(typeof(UpdateableDbContext)))
                {
                    return (UpdateableDbContext)Activator.CreateInstance(map[modelType]);
                }
                else
                {
                    throw new Exception(string.Format("The specified value ({0}) for your key ({1}) was not of the UpdateableDbContext base class.",
                        map[modelType].ToString(), modelType.ToString()));
                }
            }
            throw new Exception(string.Format("Cannot find the specified UpdateableDbContext type ({0}).", modelType.ToString()));
        }
    }
}
