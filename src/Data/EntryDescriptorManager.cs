using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EntryDescriptorManager : DbQueryPrinciple, IEntryDescriptorManager
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public EntryDescriptorManager(DbQueryOperationContext operationContext)
            : base(operationContext)
        {
            if (!operationContext.Items.ContainsKey(CACHEKEY))
                operationContext.Items.Add(CACHEKEY, new NameValueCollection<Type, IEntryDescriptor>());

            _innerCacheRepository = (operationContext.Items[CACHEKEY] as INameValueCollection<Type, IEntryDescriptor>);
        }
        #endregion

        #region Fields
        const string                                               CACHEKEY                 = "_org_core_data_entrydescriptormanager";
        readonly INameValueCollection<Type, IEntryDescriptor>     _innerCacheRepository     = default(INameValueCollection<Type, IEntryDescriptor>);
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEntryDescriptor GetDescriptor(object entity)
        {
            var _type = entity.GetType();

            if (!_innerCacheRepository.ContainsKey(_type))
            {
                _innerCacheRepository.Add(_type, new EntryDescriptor(_type));
            }

            return _innerCacheRepository[_type];
        }
        #endregion
    }
}
