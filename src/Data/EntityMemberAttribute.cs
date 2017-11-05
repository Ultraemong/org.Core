using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EntityMemberAttribute : Attribute, IEntityMember
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public EntityMemberAttribute(string name)
            : base()
        {
            _name = name;
        }
        #endregion

        #region Fields
        readonly string _name = null;
        #endregion

        #region Properties
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
        #endregion
    }
}
