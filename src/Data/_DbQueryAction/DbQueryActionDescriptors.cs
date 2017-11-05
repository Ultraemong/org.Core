using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using org.Core.Reflection;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryActionDescriptors : AttributeMemberDescriptors<DbQueryActionDescriptor, DbQueryActionDescriptors>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public DbQueryActionDescriptors(IEnumerable<DbQueryActionDescriptor> collection)
            : base(collection)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public DbQueryActionDescriptors GetDescriptorsByQueryAction(Expression<Func<IDbQueryAction, bool>> selector)
        {
            var _selector = selector.Compile();

            return new DbQueryActionDescriptors(this.Where(x => _selector(x)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <param name="propertyDirection"></param>
        /// <returns></returns>
        public DbQueryActionDescriptors GetDescriptors(DbQueryActions queryAction, DbQueryPropertyDirections propertyDirection)
        {
            return GetDescriptorsByQueryAction(x => x.QueryAction.HasFlag(queryAction) && x.PropertyDirection.HasFlag(propertyDirection));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public DbQueryActionDescriptors GetDescriptorsByQueryAction(DbQueryActions queryAction)
        {
            return GetDescriptorsByQueryAction(x => x.QueryAction.HasFlag(queryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDirection"></param>
        /// <returns></returns>
        public DbQueryActionDescriptors GetDescriptorsByPropertyDirection(DbQueryPropertyDirections propertyDirection)
        {
            return GetDescriptorsByQueryAction(x => x.PropertyDirection.HasFlag(propertyDirection));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public bool IsDeclared(Expression<Func<IDbQueryAction, bool>> selector)
        {
            var _selector = selector.Compile();

            foreach (var _descriptor in this)
            {
                if (_selector(_descriptor))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public bool IsDeclared(DbQueryActions queryAction)
        {
            return IsDeclared(x => x.QueryAction.HasFlag(queryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDirection"></param>
        /// <returns></returns>
        public bool IsDeclared(DbQueryPropertyDirections propertyDirection)
        {
            return IsDeclared(x => x.PropertyDirection.HasFlag(propertyDirection));
        }
        #endregion
    }
}
