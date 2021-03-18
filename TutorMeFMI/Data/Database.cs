using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace TutorMeFMI.Data
{
    public class Database
    {
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Secrets.ConnectionString);
        }

        public QueryFactory GetQueryFactory()
        {
            return new(GetConnection(), new MySqlCompiler());
        }
        
    }
}