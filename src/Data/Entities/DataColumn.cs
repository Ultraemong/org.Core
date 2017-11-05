using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Data;

namespace org.Core.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [EntityMember("$Column")]
    public class DataColumn
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DataColumn()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [DbQueryProperty]
        public string Name
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [DbQueryProperty]
        public int Size
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DbQueryProperty]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DbQueryProperty]
        public int Scale
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DbQueryProperty]
        public int Precision
        {
            get;
            set;
        }
        #endregion
    }
}
