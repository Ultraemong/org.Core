using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAttributeDescriptorProvider
    {
        /// <summary>
        /// 
        /// </summary>
        AttributeMemberDescriptors AttributeDescriptors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        AttributeMemberDescriptor GetAttributeDescriptorByAttributeType(Type attributeType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        AttributeMemberDescriptors GetAttributeDescriptorsByAttributeType(Type attributeType);
    }
}
