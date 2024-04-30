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
    public class AddressRepository : IBaseRepository<Address>
    {
        private readonly BaseOptions _options;

        private readonly IDbConnection _connection;

        public AddressRepository(BaseOptions options)
        {
            _options = options;
        }

        public async Task<IQueryable<Address>> GetData(CancellationToken? cancellation = null, Expression<Func<Address, bool>> predicate = null)
        {
            using (IDbConnection db = new SqlConnection(_options.ConnectionString))
            {
                db.Open();

                var sqlQuery = "SELECT * FROM " + typeof(Address).Name;

                if (predicate != null)
                {
                    var whereClause = ((dynamic)predicate.Body).Body.ToString();

                    sqlQuery += " WHERE " + whereClause;
                }

                IEnumerable<Address> result = await db.QueryAsync<Address>(sqlQuery, cancellation);

                return result.AsQueryable();

            }
        }
    }
}
