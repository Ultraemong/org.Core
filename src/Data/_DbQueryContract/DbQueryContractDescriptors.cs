
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryContractDescriptors : AttributeMemberDescriptors<DbQueryContractDescriptor, DbQueryContractDescriptors>, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="ownerDescriptor"></param>
        public DbQueryContractDescriptors(IEnumerable<DbQueryContractDescriptor> collection, IEntryDescriptor ownerDescriptor)
            : base(collection)
        {
            _ownerDescriptor = ownerDescriptor;
        } 
        #endregion

        #region Fields
        readonly IEntryDescriptor   _ownerDescriptor            = null;
        DbQueryContractDescriptors  _customContractDescriptors  = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IEntryDescriptor DeclaringDescriptor
        {
            get 
            {
                return _ownerDescriptor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryContractDescriptors CustomContractDescriptors
        {
            get
            {
                if (null == _customContractDescriptors)
                    return _customContractDescriptors = GetDescriptorsFor<IDbQueryContract>(x => x.QueryAction.Equals(DbQueryActions.Custom));

                return _customContractDescriptors;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public DbQueryContractDescriptor GetDescriptorByAction(DbQueryActions queryAction)
        {
            return GetDescriptorFor<IDbQueryContract>(x => x.QueryAction.Equals(queryAction));
        }
        #endregion
    }
}
