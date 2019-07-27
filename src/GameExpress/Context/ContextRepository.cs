using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Context
{
    /// <summary>
    /// Verzeichnis aller Kontexte
    /// </summary>
    public static class ContextRepository
    {
        /// <summary>
        /// Das Repository
        /// </summary>
        private static readonly IDictionary<Type, IItemContext> Repository = new Dictionary<Type, IItemContext>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="context"></param>
        public static void RegisterContext(Type item, IItemContext context)
        {
            if (!Repository.ContainsKey(item))
            {
                Repository[item] = context;
            }
            Repository[item] = context;
        }

        /// <summary>
        /// Finde einen Kontext
        /// </summary>
        /// <param name="item">Der mit dem Item verknüpfte Kontext oder null</param>
        /// <returns></returns>
        public static IItemContext FindContext(Item item)
        {
            var type = item?.GetType();
            if (type != null && Repository.ContainsKey(type))
            {
                return Repository[type];
            }

            return null;
        }
    }
}
