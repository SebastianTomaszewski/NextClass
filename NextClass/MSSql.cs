using System.Data.SqlClient;
using NextClass.Model;

namespace NextClass
{
    public class MsSql
    {
        private SqlConnection _conn;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _connStr;


        #region ctr

        public MsSql(ConnectionStringModel connectionStringModel)
        {
            _connStr = connectionStringModel.ConnectionString;
        }

        public MsSql(string connectionString)
        {
            _connStr = connectionString;
        }

        #endregion

        #region Public Method

        public bool IsConnection()
        {
            using (_conn = new SqlConnection(_connStr))
            {
                try
                {
                    _conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion







    }
}
