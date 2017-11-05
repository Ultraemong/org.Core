using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SByteDescriptor : IValueDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public SByteDescriptor()
        {
        }
        #endregion

        #region Fields
        readonly sbyte _minValue        = sbyte.MinValue;
        readonly sbyte _maxValue        = sbyte.MaxValue;
        readonly sbyte _defaultValue    = 0;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object MininumValue
        {
            get
            {
                return _minValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object MaxinumValue
        {
            get
            {
                return _maxValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return _defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNumeric
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInteger
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFloat
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBoolean
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
