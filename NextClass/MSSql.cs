using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using NextClass.Extensions;
using NextClass.Model;
using static System.String;

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

        public void ExecSql(string query)
        {
            try
            {
                using (_conn = new SqlConnection(_connStr))
                {
                    if (_conn.State != ConnectionState.Closed) _conn.Close();
                  
                        SqlCommand command = new SqlCommand(query, _conn);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                catch
                {
                    throw new Exception();
                }

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

        #endregion
        // Set the connection, command, and then execute the command with non query.
        public int ExecuteNonQuery( string cmdText,CommandType cmdType, params SqlParameter[] parameters)
        {
            using ( _conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand(cmdText, _conn))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);

                    _conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        
        // Set the connection, command, and then execute the command and only return one value.
        public object ExecuteScalar( string cmdText,CommandType cmdType, params SqlParameter[] parameters)
        {
            using (_conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand(cmdText, _conn))
                {
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(parameters);

                    _conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        
        // Set the connection, command, and then execute the command with query and return the reader.
        public SqlDataReader ExecuteReader( string cmdText,
            CommandType cmdType, params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(_connStr);

            using (var cmd = new SqlCommand(cmdText, conn))
            {
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                // IDataReader is closed.
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }
        //public DataTable ExecuteReader(string cmdText,CommandType cmdType, params SqlParameter[] parameters)
        //{
        //    var conn = new SqlConnection(_connStr);
        //    DataTable dt = new DataTable();
        //    using (var cmd = new SqlCommand(cmdText, conn))
        //    {
        //        cmd.CommandType = cmdType;
        //        cmd.Parameters.AddRange(parameters);

        //        conn.Open();
        //        // When using CommandBehavior.CloseConnection, the connection will be closed when the 
        //        // IDataReader is closed.
        //        var reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
        //        dt.Load(cmd.ExecuteReader(CommandBehavior.SingleRow));

        //        return dt;
        //    }
        //}
        // "exec [PSA_Traceability].[dbo].[Ask_SN] 'PSA','20800','38123456','38234345BP3456789012'"
        public DataTable ExecStoredProcedure(string db, string procName, params string[] args)
        {
            var dt = new DataTable();
            string cmdText =$@"exec {db.ToQuote(']')}.[dbo].{procName.ToQuote(']')} {args.ToQuoteAndCommaSplitStrings()}";

           //!: zwracac wynik jako obiekt ?


            using (_conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand(cmdText, _conn))
                {
                    using (var da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        _conn.Open();
                        da.Fill(dt);
                            
                        return dt;
                    }
                }
            }
        }



      
        
    }
}
