using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NextClass.Model;
using static System.Data.SqlDbType;
using static System.String;

namespace NextClass
{
    public class Traceability
    {

        private SqlConnection _conn;
        private MsSql _msSql;
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

        //EXEC	[dbo].[ASK_SN]	@LINE = N'JUNIOR',	@OP = 750, @PN = N'26111047',	@SN = N'26111047062173033700'
        public SqlDataReader Ask_SN(string line, string op, string pn, string sn)
        {

            var paramList = new List<SqlParameter>
            {
                new SqlParameter("@LINE", VarChar, 50) {Value = line, IsNullable = false},
                new SqlParameter("@OP", Int) {Value = op, IsNullable = false},
                new SqlParameter("@PN", VarChar, 50) {Value = pn, IsNullable = true},
                new SqlParameter("@SN", VarChar, 50) {Value = sn, IsNullable = false}
            };

            return _msSql.ExecuteReader("[dbo].[ASK_SN]", CommandType.StoredProcedure, paramList.ToArray());
        }
        public SqlDataReader Ask_SN(string line, string op, string pn, string sn, string[] parm )
        {
            if (IsNullOrWhiteSpace(line)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(line));
            if (IsNullOrWhiteSpace(op))   throw new ArgumentException("Value cannot be null or whitespace.", nameof(op));
            if (IsNullOrWhiteSpace(sn))   throw new ArgumentException("Value cannot be null or whitespace.", nameof(sn));
            if (parm.Length == 0)         throw new ArgumentException("Value cannot be an empty collection.", nameof(parm));
            if (pn == null)               throw new ArgumentNullException(nameof(pn));

            var list = new List<SqlParameter>
            {
                new SqlParameter("@LINE", VarChar, 50) {Value = line, IsNullable = false},
                new SqlParameter("@OP", Int) {Value = op, IsNullable = false},
                new SqlParameter("@PN", VarChar, 50) {Value = pn, IsNullable = true},
                new SqlParameter("@SN", VarChar, 50) {Value = sn, IsNullable = false}
            };

            if (parm.Length != 0) list.AddRange(parm.Select((t, i) => new SqlParameter($"@EXTNAME{i:D2}0", VarChar, 100) {Value = t, IsNullable = true}));

            return _msSql.ExecuteReader("[dbo].[ASK_SN]", CommandType.StoredProcedure, list.ToArray());
        }

        public int ADD_TO_BLACKLIST(string sn, string code, string remarks)
        {//ToDo: dopytać czy do blacklisty można dodać osobę blokującą?
            if (IsNullOrWhiteSpace(sn)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(sn));
            if (IsNullOrWhiteSpace(code))throw new ArgumentException("Value cannot be null or whitespace.", nameof(code));
            if (IsNullOrWhiteSpace(remarks))throw new ArgumentException("Value cannot be null or whitespace.", nameof(remarks));
            

            var list = new List<SqlParameter>
            {
                new SqlParameter("@LINE", VarChar, 100) {Value = sn, IsNullable = false},
                new SqlParameter("@PN", VarChar, 50) {Value = code, IsNullable = false},
                new SqlParameter("@SN", VarChar, 500) {Value = remarks, IsNullable = false}
            };

         //   EXEC[dbo].[ADD_TO_BLACKLIST] @sn = N'26111047062173033600',@code = N'test',@remarks = N'testtesttest'

                return _msSql.ExecuteNonQuery("[dbo].[ADD_TO_BLACKLIST]", CommandType.StoredProcedure, list.ToArray());


        }
    }
}
