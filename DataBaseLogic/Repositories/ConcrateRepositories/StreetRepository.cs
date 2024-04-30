using Dapper;
using DataBaseLogic.Entities;
using DataBaseLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogic.Repositories.ConcrateRepositories
{
    public class StreetRepository : IBaseRepository<Street>
    {
        private readonly BaseOptions _options;

        private readonly IDbConnection _connection;

        public StreetRepository(BaseOptions options)
        {
            _options = options;
        }

        public async Task<IQueryable<Street>> GetData(CancellationToken? cancellation = null, Expression<Func<Street, bool>> predicate = null)
        {
            using (IDbConnection db = new SqlConnection(_options.ConnectionString))
            {
                db.Open();

                var sqlQuery = "SELECT * FROM " + typeof(Street).Name;

                if (predicate != null)
                {
                    var whereClause = ((dynamic)predicate.Body).Body.ToString();

                    sqlQuery += " WHERE " + whereClause;
                }

                IEnumerable<Street> result = await db.QueryAsync<Street>(sqlQuery, cancellation);

                return result.AsQueryable();

            }
        }
    }
}
