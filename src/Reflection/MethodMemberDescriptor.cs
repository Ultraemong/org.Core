using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using org.Core.Utilities;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class MethodMemberDescriptor : MemberDescriptor<MethodInfo>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        public MethodMemberDescriptor(MethodInfo methodInfo)
            : base(methodInfo)
        {   
        } 
        #endregion

        #region Fields
        #endregion
        
        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Invoke(object instance, params object[] parameters)
        {
            var _parameters     = Member.GetParameters();
            var _canInvoke      = TypeComparisonUtils.Compare(_parameters, parameters);

            if (_canInvoke)
                return Member.Invoke(instance, parameters);

            return null;
        }
        #endregion
    }
}
