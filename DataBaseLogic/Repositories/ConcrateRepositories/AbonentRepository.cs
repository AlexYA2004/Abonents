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
    public class AbonentRepository : IBaseRepository<Abonent>
    {
        private readonly BaseOptions _options;

        public AbonentRepository(BaseOptions options)
        {
            _options = options;
        }


        public async Task<IQueryable<Abonent>> GetData(CancellationToken? cancellation = null, Expression<Func<Abonent, bool>> predicate = null)
        {
            using (IDbConnection db = new SqlConnection(_options.ConnectionString))
            {
                db.Open();

                var sqlQuery = "SELECT * FROM " + typeof(Abonent).Name;

                if (predicate != null)
                {
                    var whereClause = ((dynamic)predicate.Body).Body.ToString();

                    sqlQuery += " WHERE " + whereClause;
                }

                IEnumerable<Abonent> result = await db.QueryAsync<Abonent>(sqlQuery, cancellation);

                return result.AsQueryable();

            }
        }
    }
}
