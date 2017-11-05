using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

using org.Core.Reflection;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryOperationDescriptors : MethodMemberDescriptors<DbQueryOperationDescriptor, DbQueryOperationDescriptors>, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="declaringDescriptor"></param>
        public DbQueryOperationDescriptors(IEnumerable<DbQueryOperationDescriptor> collection, IEntryDescriptor declaringDescriptor)
            : base(collection)
        {
            _declaringDescriptor = declaringDescriptor;
        } 
        #endregion

        #region Fields
        readonly IEntryDescriptor _declaringDescriptor = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IEntryDescriptor DeclaringDescriptor
        {
            get
            {
                return _declaringDescriptor;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public DbQueryOperationDescriptors GetDescriptorsByQueryAction(DbQueryActions queryAction)
        {
            return GetDescriptorsByQueryOperation(x => x.QueryAction.HasFlag(queryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public DbQueryOperationDescriptors GetDescriptorsByQueryOperation(Expression<Func<IDbQueryOperation, bool>> selector)
        {
            var _selector = selector.Compile();

            return new DbQueryOperationDescriptors(this.Where(x => _selector(x)), DeclaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbQueryOperationDescriptors OrderByAscending()
        {
            return new DbQueryOperationDescriptors(this.OrderBy(x => x.Order), DeclaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbQueryOperationDescriptors OrderByDescending()
        {
            return new DbQueryOperationDescriptors(this.OrderByDescending(x => x.Order), DeclaringDescriptor);
        }
        #endregion
    }
}
