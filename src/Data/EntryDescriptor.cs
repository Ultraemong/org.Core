using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections;

using org.Core.Reflection;
using org.Core.Utilities;
using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EntryDescriptor : IEntryDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryType"></param>
        internal EntryDescriptor(Type entryType)
        {
            _declaringType      = entryType;

            _isListType         = ObjectUtils.IsListType(entryType);
            _isSealedType       = entryType.IsSealed;            

            _namespace          = entryType.Namespace;

            var _descriptor     = GetAttributeDescriptorByAttributeType(typeof(IEntityMember));

            if (null != _descriptor)
                _entityMember   = _descriptor.Member as IEntityMember;

            if (null == _entityMember)
                _entityMember   = new EntityMember(entryType.Name);
        }
        #endregion

        #region Fields
        readonly IEntityMember              _entityMember                   = null;

        readonly string                     _namespace                      = null;
        readonly Type                       _declaringType                  = null;

        readonly bool                       _isListType                     = false;
        readonly bool                       _isSealedType                   = false;

        IDataServiceContract                _serviceContract                = null;
        AttributeMemberDescriptors          _attributeDescriptors           = null;
        DbQueryContractDescriptors          _contractDescriptors            = null;
        DbQueryPropertyDescriptors          _propertyDescriptors            = null;
        DbQueryOperationDescriptors         _operationDescriptors           = null;
        IgnoreChildPropertyDescriptors      _ignorePropertyDescriptors      = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Type DeclaringType
        {
            get
            {
                return _declaringType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return _entityMember.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FullName 
        { 
            get
            {
                return string.Format("{0}.{1}", Namespace, Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsListType
        {
            get 
            {
                return _isListType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSealedType
        {
            get 
            {
                return _isSealedType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDataServiceContract ServiceContract
        {
            get
            {
                if (null == _serviceContract)
                    return _serviceContract = DataServiceContractHelpers.RetrieveDataServiceContract(AttributeDescriptors);

                return _serviceContract;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryContractDescriptors ContractDescriptors
        {
            get
            {
                if (null == _contractDescriptors)
                    return _contractDescriptors = DbQueryContractHelpers.RetrieveMemberDescriptors(AttributeDescriptors, this);

                return _contractDescriptors;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperationDescriptors OperationDescriptors
        {
            get
            {
                if (null == _operationDescriptors)
                    return _operationDescriptors = DbQueryOperationHelpers.RetrieveMemberDescriptors(_declaringType, this);

                return _operationDescriptors;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AttributeMemberDescriptors AttributeDescriptors
        {
            get
            {
                if (null == _attributeDescriptors)
                    return _attributeDescriptors = AttributeMemberHelpers.RetrieveMemberDescriptors(_declaringType, true);

                return _attributeDescriptors;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDescriptors PropertyDescriptors
        {
            get
            {
                if (null == _propertyDescriptors)
                    return _propertyDescriptors = DbQueryPropertyHelpers.RetrieveMemberDescriptors(_declaringType, this);

                return _propertyDescriptors;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IgnoreChildPropertyDescriptors IgnorePropertyDescriptors
        {
            get 
            {
                if (null == _ignorePropertyDescriptors)
                    return _ignorePropertyDescriptors = IgnoreChildPropertyHelpers.RetrieveMemberDescriptors(_declaringType, this);

                return _ignorePropertyDescriptors;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public AttributeMemberDescriptor GetAttributeDescriptorByAttributeType(Type attributeType)
        {
            return AttributeDescriptors.GetDescriptorByAttributeType(attributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public AttributeMemberDescriptors GetAttributeDescriptorsByAttributeType(Type attributeType)
        {
            return AttributeDescriptors.GetDescriptorsByAttributeType(attributeType);
        }
        #endregion
    }
}
