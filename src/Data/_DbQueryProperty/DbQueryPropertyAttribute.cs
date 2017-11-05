using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DbQueryPropertyAttribute : Attribute, IDbQueryProperty
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <param name="size"></param>
        public DbQueryPropertyAttribute(SqlDbType sqlDbType, int size)
            : base()
        {
            _sqlDbType  = sqlDbType;
            _size       = size;
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public DbQueryPropertyAttribute(SqlDbType sqlDbType, int size, byte precision, byte scale)
            : this(sqlDbType, size)
        {
            _precision   = precision;
            _scale       = scale;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sqlDbType"></param>
        /// <param name="size"></param>
        public DbQueryPropertyAttribute(string name, SqlDbType sqlDbType, int size)
            : this(sqlDbType, size)
        {
            _name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sqlDbType"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public DbQueryPropertyAttribute(string name, SqlDbType sqlDbType, int size, byte precision, byte scale)
            : this(name, sqlDbType, size)
        {
            _precision  = precision;
            _scale      = scale;
        }
        #endregion

        #region Fields
        bool        _isNullable             = false;
        bool        _isMappable             = true;
        bool        _isPrimaryKey           = false;
        bool        _ignoreXmlDataMember    = false;

        int         _order                  = 0;
        int         _size                   = -1;

        string      _name                   = null;

        byte        _precision              = 18;
        byte        _scale                  = 0;

        SqlDbType   _sqlDbType              = SqlDbType.BigInt;

        object      _defaultValue           = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Prefix
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlDbType DbType
        {
            get
            {
                return _sqlDbType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                switch(DbType)
                {
                    case SqlDbType.Xml:
                        return 0;

                    case SqlDbType.VarChar:
                    case SqlDbType.NVarChar:
                        return _size.Equals(int.MaxValue) ? -1 : _size;
                }

                return _size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Precision
        {
            get
            {
                return _precision;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Scale
        {
            get
            {
                return _scale;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNullable
        {
            get
            {
                return _isNullable;
            }

            set
            {
                _isNullable = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMappable
        {
            get
            {
                return _isMappable;
            }

            set
            {
                _isMappable = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPrimaryKey
        {
            get
            {
                return _isPrimaryKey;
            }

            set
            {
                _isPrimaryKey = value;
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

            set
            {
                _defaultValue = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Order
        {
            get
            {
                return _order;
            }

            set
            {
                _order = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IgnoreXmlDataMember
        {
            get
            {
                return _ignoreXmlDataMember;
            }

            set
            {
                _ignoreXmlDataMember = value;
            }
        }
        #endregion
    }
}
