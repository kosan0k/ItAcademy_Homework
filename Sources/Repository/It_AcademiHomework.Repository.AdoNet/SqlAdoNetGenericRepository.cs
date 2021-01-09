using It_AcademyHomework.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace It_AcademyHomework.Repository.AdoNet
{
    public class SqlAdoNetGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private string _dbConnectionString;
        private IReadOnlyDictionary<string, string> _propertyDefinitions;
        private IReadOnlyDictionary<string, string> _entitiyTypeDefinitions;

        /// <summary>
        /// Creates new instance of AdoNetGenericRepository class. May use propertyDefinitions parameter to use custom column name for equivalent of T instance property
        /// </summary>
        /// <param name="dbConnectionString">Database connection metadata</param>
        /// <param name="entitiyTypeDefinitions">Dictionary with model types and their table name equivalent</param>
        /// <param name="propertyDefinitions">Dictionary with property names and their column name equivalent</param>
        public SqlAdoNetGenericRepository(string dbConnectionString,
            IReadOnlyDictionary<string, string> entitiyTypeDefinitions = null,
            IReadOnlyDictionary<string,string> propertyDefinitions = null)
        {
            _dbConnectionString = !string.IsNullOrEmpty(dbConnectionString) ? dbConnectionString : throw new ArgumentNullException(nameof(dbConnectionString));
            _propertyDefinitions = propertyDefinitions;

            _entitiyTypeDefinitions = entitiyTypeDefinitions;
        }

        #region IGenericRepository implementation
        public async Task<RepositoryOperationResult> AddAsync(T itemToAdd)
        {
            RepositoryOperationResult result = null;

            if (itemToAdd != null)
            {
                try
                {
                    var itemProperties = itemToAdd.GetType().GetProperties();
                    var tableName = GetTableName(itemToAdd);

                    var sqlCommandStringBuilder = new StringBuilder($"INSERT INTO {tableName}");
                    AppendColumnNames(sqlCommandStringBuilder, itemProperties);
                    sqlCommandStringBuilder.Append(" VALUES");
                    AppendColumnValues(sqlCommandStringBuilder, itemProperties, itemToAdd);
                    sqlCommandStringBuilder.Append(';');

                    await using var sqlConnection = new SqlConnection(_dbConnectionString);
                    await sqlConnection.OpenAsync();
                    var sqlInsertCommand = new SqlCommand(sqlCommandStringBuilder.ToString(), sqlConnection);
                    await sqlInsertCommand.ExecuteNonQueryAsync();

                    result = new RepositoryOperationResult(true);
                }
                catch (Exception e)
                {
                    result = new RepositoryOperationResult(false, new List<string>() { e.Message });
                }
            }
            else
            {
                result = new RepositoryOperationResult(false, new List<string>() { "Parameter can not be null" });
            }

            return result;
        }

        private string GetTableName(T item)
        {
            var entityType = typeof(T).Name;
            return _entitiyTypeDefinitions.TryGetValue(entityType, out string tableName) ? tableName : entityType;
        }

        private void AppendColumnNames(StringBuilder sqlQueryStringBuilder, PropertyInfo[] itemProperties)
        {
            sqlQueryStringBuilder.Append('(');
            foreach (var property in itemProperties)
            {
                var columnName = _propertyDefinitions!=null && _propertyDefinitions.TryGetValue(property.Name, out string definition)
                    ? definition
                    : property.Name;
                sqlQueryStringBuilder.Append(columnName).Append(',');
            }
            sqlQueryStringBuilder.Remove(sqlQueryStringBuilder.Length - 1, 1);

            sqlQueryStringBuilder.Append(')');
        }

        private void AppendColumnValues(StringBuilder sqlQueryStringBuilder, PropertyInfo[] itemProperties, T item)
        {
            sqlQueryStringBuilder.Append('(');
            foreach (var property in itemProperties)
            {
                var propertyValue = $"'{property.GetValue(item)}'";
                if (propertyValue is double)
                {
                    propertyValue = propertyValue.ToString().Replace(',','.');
                }

                sqlQueryStringBuilder.Append(propertyValue).Append(',');
            }
            sqlQueryStringBuilder.Remove(sqlQueryStringBuilder.Length - 1, 1);

            sqlQueryStringBuilder.Append(')');
        }

        public Task<RepositoryOperationResult> DeleteAsync(T itemToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> queryFilter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RepositoryOperationResult> UpdateAsync(T itemToUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IDispose implementation

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
