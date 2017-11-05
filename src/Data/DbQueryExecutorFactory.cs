

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryExecutorFactory : IDbQueryExecutorFactory
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DbQueryExecutorFactory()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public IDbQueryExecutor CreateDbQueryExecutor(DbQueryBehaviors queryBehavior)
        {
            if (queryBehavior.HasFlag(DbQueryBehaviors.SchemaOnly))
                return new SchemaOnlyQueryExecutor();

            else if (queryBehavior.HasFlag(DbQueryBehaviors.SingleRow))
                return new SingleRowQueryExecutor();

            else if (queryBehavior.HasFlag(DbQueryBehaviors.MultipleRows))
                return new MultipleRowsQueryExecutor();

            else if (queryBehavior.HasFlag(DbQueryBehaviors.ScalarValue))
                return new ScalarValueQueryExecutor();
            
            return new DefaultQueryExecutor();
        }
        #endregion
    }
}
