using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyMemberDescriptor : MemberDescriptor<PropertyInfo>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        public PropertyMemberDescriptor(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {   
        } 
        #endregion

        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return (Member.CanRead && !Member.CanWrite);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type RetrunType
        {
            get
            {
                return Member.PropertyType;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public object GetValue(object instance, bool nonPublic = false)
        {
            if (Member.CanRead)
            {
                var _method = Member.GetGetMethod(nonPublic);

                if (null != _method)
                    return _method.Invoke(instance, null);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public object SetValue(object instance, object value, bool nonPublic = false)
        {
            if (Member.CanWrite)
            {
                var _method = Member.GetSetMethod(nonPublic);

                if (null != _method)
                    return _method.Invoke(instance, new object[] { value });
            }

            return null;
        }
        #endregion
    }
}
