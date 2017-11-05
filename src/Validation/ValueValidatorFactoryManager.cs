using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;
using org.Core.Diagnostics;

namespace org.Core.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValueValidatorFactoryManager
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static ValueValidatorFactoryManager()
        {
        }
        #endregion

        #region Fields
        static IValueValidatorFactory s_factory = null;
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IValueValidatorFactory CreateValueValidatorFactory()
        {
            if(null == s_factory)
                return s_factory = new ValueValidatorFactory();

            return s_factory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IValueValidatorFactory GetValueValidatorFactory()
        {
            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(2);
            var _factoryProvider    = typeof(IValueValidatorFactoryProvider);
            var _memberDescriptor   = (AttributeMemberDescriptor)null;

            if (null != _stackFrame)
            {
                if (_stackFrame.MethodDescriptor.HasAttributeMembers)
                {
                    _memberDescriptor = _stackFrame.MethodDescriptor.GetAttributeDescriptorByAttributeType(_factoryProvider);

                    if (null != _memberDescriptor)
                        return (_memberDescriptor.Member as IValueValidatorFactoryProvider).ValueValidatorFactory;
                }

                var _declaringType  = _stackFrame.MethodDescriptor.Member.DeclaringType;
                var _descriptors    = AttributeMemberHelpers.RetrieveMemberDescriptors(_declaringType, _factoryProvider);

                if (null != _descriptors)
                {
                    _memberDescriptor = _descriptors.GetDescriptorByAttributeType(_factoryProvider);

                    return (_memberDescriptor.Member as IValueValidatorFactoryProvider).ValueValidatorFactory;
                }
            }

            return CreateValueValidatorFactory();
        }
        #endregion
    }
}
