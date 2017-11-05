using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Validation;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryPropertyValidator : DbQueryOperatingPrinciple, IDbQueryPropertyValidator
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSession"></param>
        /// <param name="propertyDescriptors"></param>
        public DbQueryPropertyValidator(DbQueryOperatingSession operatingSession, DbQueryPropertyDescriptors propertyDescriptors)
            : base(operatingSession)
        {
            _propertyDescriptors  = propertyDescriptors;
        } 
        #endregion

        #region Events
        public event DbQueryFailedEventHandler      Failed                  = null;
        public event DbQueryValidatedEventHandler   Validated               = null;
        #endregion

        #region Fields
        readonly DbQueryPropertyDescriptors         _propertyDescriptors    = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDescriptors PropertyDescriptors
        {
            get
            {
                return _propertyDescriptors;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Validate(object entity)
        {
            foreach (var _memberDescriptor in PropertyDescriptors)
            {
                var _isRequired = _memberDescriptor.ActionDescriptors.IsDeclared(x => x.IsRequired 
                    && x.PropertyDirection.HasFlag(DbQueryPropertyDirections.Input));

                if (_isRequired)
                {
                    var _value = _memberDescriptor.GetValue(entity);

                    if (!ValueValidator.Validate(_value))
                    {
                        if (null != Failed)
                        {
                            var _message    = string.Format("The '{0}' property cannot be null.", _memberDescriptor.Name);
                            var _exception  = new DbQueryException(_message);

                            Failed(this, new DbQueryFailedEventArgs(_exception));
                        }
                    }

                    if (null != Validated)
                    {
                        Validated(this, EventArgs.Empty);
                    }
                }
            }

            return true;
        } 
        #endregion
    }
}
