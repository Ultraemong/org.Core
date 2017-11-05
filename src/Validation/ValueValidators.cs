using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;
using System.Xml;

namespace org.Core.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class ValueValidators : NameValueCollection<Type, IValueValidator> 
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        private ValueValidators()
            : base ()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static ValueValidators()
        {
            s_validators = new ValueValidators()
            {
                {typeof(byte),      new ByteValidator()},
                {typeof(DateTime),  new DateTimeValidator()},
                {typeof(decimal),   new DecimalValidator()},
                {typeof(double),    new DoubleValidator()},
                {typeof(short),     new Int16Validator()},
                {typeof(int),       new Int32Validator()},
                {typeof(long),      new Int64Validator()},
                {typeof(sbyte),     new SbyteValidator()},
                {typeof(float),     new SingleValidator()},
                {typeof(string),    new StringValidator()},
                {typeof(TimeSpan),  new TimeSpanValidator()},
                {typeof(uint),      new UInt32Validator()},
                {typeof(ulong),     new UInt64Validator()},
                {typeof(ushort),    new UInt16Validator()},
                {typeof(Guid),      new GuidValidator()},
            };
        }
        #endregion

        #region Fields
        static readonly ValueValidators s_validators = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static ValueValidators Validators
        {
            get
            {
                return s_validators;
            }
        }
        #endregion
    }
}
