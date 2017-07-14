using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AccountService.Repositories
{
    public abstract class SqlRepository
    {
        private readonly string _appDbConnectionString;

        public SqlRepository(IOptionsSnapshot<AccountServiceOptions> options)
        {
            _appDbConnectionString = options.Value.AppDBConnection;
        }

        public async Task<SqlConnection> GetConnection()
        {
            var conn = new SqlConnection(_appDbConnectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
