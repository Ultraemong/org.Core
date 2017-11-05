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
    public sealed class EntityMember : IEntityMember
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal EntityMember(string name)
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
