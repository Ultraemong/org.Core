using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;
using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntryDescriptor : IDataServiceContractProvider, IDbQueryOperationProvider, IDbQueryPropertyProvider, IAttributeDescriptorProvider, IDbQueryContractProvider, IIgnoreChildPropertyProvider
    {
        /// <summary>
        /// 
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// 
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsListType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsSealedType { get; }
    }
}
