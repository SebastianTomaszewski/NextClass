using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NextClass.Model;
using static System.Int32;

namespace NextClass
{
    public class Traceability
    {

        private SqlConnection _conn;
        private MsSql _msSql;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _connStr;


        #region ctr

        public Traceability(ConnectionStringModel connectionStringModel)
        {
            _connStr = connectionStringModel.ConnectionString;
            _msSql = new MsSql(connectionStringModel);
        }

        public Traceability(string connectionString)
        {
            _connStr = connectionString;
            _msSql = new MsSql(_connStr);
        }

        #endregion

        //EXEC	[dbo].[ASK_SN]	@LINE = N'JUNIOR',	@OP = 750, @PN = N'26111047',	@SN = N'26111047062173033700'
        public object Ask_SN(string line,string op, string pn, string sn)
        {

            var paramList = new List<SqlParameter>
            {
                new SqlParameter("@LINE", SqlDbType.VarChar, 50) {Value = line, IsNullable = false},
                new SqlParameter("@OP", SqlDbType.Int) {Value = op,IsNullable = false},
                new SqlParameter("@PN", SqlDbType.VarChar, 50) {Value = pn,IsNullable = true},
                new SqlParameter("@SN", SqlDbType.VarChar, 50) {Value = sn,IsNullable = false}
            };


            //@EXTNAME000 varchar(100) = NULL,
            //@EXTNAME010 varchar(100) = NULL,
            //@EXTNAME020 varchar(100) = NULL,
            //@EXTNAME030 varchar(100) = NULL,
            //@EXTNAME040 varchar(100) = NULL,
            //@EXTNAME050 varchar(100) = NULL,
            //@EXTNAME060 varchar(100) = NULL,
            //@EXTNAME070 varchar(100) = NULL,
            //@EXTNAME080 varchar(100) = NULL,
            //@EXTNAME090 varchar(100) = NULL,
            //@EXTNAME100 varchar(100) = NULL,
            //@EXTNAME110 varchar(100) = NULL,
            //@EXTNAME120 varchar(100) = NULL,
            //@EXTNAME130 varchar(100) = NULL,
            //@EXTNAME140 varchar(100) = NULL,
            //@EXTNAME150 varchar(100) = NULL,
            //@EXTNAME160 varchar(100) = NULL,
            //@EXTNAME170 varchar(100) = NULL,
            //@EXTNAME180 varchar(100) = NULL,
            //@EXTNAME190 varchar(100) = NULL

 
           
            return _msSql.ExecuteReader("[dbo].[ASK_SN]", CommandType.StoredProcedure, paramList.ToArray());
        }


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


    }
}
