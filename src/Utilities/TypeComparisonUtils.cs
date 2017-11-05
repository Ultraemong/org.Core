using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeComparisonUtils
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static TypeComparisonUtils()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool Compare(ParameterInfo left, object right, bool inherit = true)
        {
            var _compare = false;

            if (null != left && null != right)
            {
                var _type1 = left.ParameterType;
                var _type2 = right.GetType();

                if (inherit)
                {
                    if (ObjectUtils.InheritanceEquals(_type1, _type2))
                    {
                        _compare = true;
                    }
                }
                else
                {
                    if (_type1.Equals(_type2))
                    {
                        _compare = true;
                    }
                }
            }

            return _compare;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool Compare(ParameterInfo[] left, object[] right, bool inherit = true)
        {
            var _compare = false;

            if (null != left && null != right)
            {
                if (left.Length.Equals(right.Length))
                {
                    _compare = true;

                    for (var i = 0; i < left.Length; i++)
                    {
                        if (!TypeComparisonUtils.Compare(left[i], right[i], inherit))
                        {
                            _compare = false;

                            break;
                        }
                    }
                }
            }

            return _compare;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool Compare(ParameterInfo left, ParameterInfo right, bool inherit = true)
        {
            var _compare = false;

            if (null != left && null != right)
            {
                var _type1 = left.ParameterType;
                var _type2 = right.ParameterType;

                if (inherit)
                {
                    if (ObjectUtils.InheritanceEquals(_type1, _type2))
                    {
                        _compare = true;
                    }
                }
                else
                {
                    if (_type1.Equals(_type2))
                    {
                        _compare = true;
                    }
                }
            }

            return _compare;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool Compare(ParameterInfo[] left, ParameterInfo[] right, bool inherit = true)
        {
            var _compare = false;

            if (null != left && null != right)
            {
                if (left.Length.Equals(right.Length))
                {
                    _compare = true;

                    for (var i = 0; i < left.Length; i++)
                    {
                        if (!TypeComparisonUtils.Compare(left[i], right[i], inherit))
                        {
                            _compare = false;

                            break;
                        }
                    }
                }
            }

            return _compare;
        }
        #endregion
    }
}
