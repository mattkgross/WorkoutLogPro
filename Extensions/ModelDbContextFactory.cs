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

        public static Type FactoryGetUpdateableDbContext(Type modelType)
        {
            if(map.ContainsKey(modelType))
            {
                return map[modelType];
            }
            throw new Exception(string.Format("Cannot find the specified UpdateableDbContext type ({0}).", modelType.ToString()));
        }
    }
}
