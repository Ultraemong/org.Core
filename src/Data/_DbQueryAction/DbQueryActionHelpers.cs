
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using org.Core.Reflection;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbQueryActionHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static DbQueryActionHelpers()
        {
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static DbQueryActionDescriptors RetrieveMemberDescriptors(object instance)
        {
            return AttributeMemberHelpers.RetrieveMemberDescriptors<DbQueryActionDescriptor, DbQueryActionDescriptors>(instance, typeof(IDbQueryAction), true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberDescriptors"></param>
        /// <returns></returns>
        public static DbQueryActionDescriptors RetrieveMemberDescriptors(AttributeMemberDescriptors memberDescriptors)
        {
            var _dummy          = new ListCollection<DbQueryActionDescriptor>();
            var _descriptors    = memberDescriptors.GetDescriptorsByAttributeType(typeof(IDbQueryAction));

            foreach (var _descriptor in _descriptors)
            {
                _dummy.Add(new DbQueryActionDescriptor(_descriptor.Member));
            }

            return new DbQueryActionDescriptors(_dummy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberDescriptors"></param>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public static DbQueryActionDescriptors RetrieveMemberDescriptors(AttributeMemberDescriptors memberDescriptors, DbQueryActions? queryAction)
        {
            var _descriptors = DbQueryActionHelpers.RetrieveMemberDescriptors(memberDescriptors);

            if (queryAction.HasValue)
                return new DbQueryActionDescriptors(_descriptors.Where(x => x.QueryAction.HasFlag(queryAction.Value)));

            return _descriptors;
        }
        #endregion
    }
}
