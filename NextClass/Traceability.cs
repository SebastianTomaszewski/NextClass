using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NextClass.Model;

namespace NextClass
{
    public class Traceability
    {

        private SqlConnection _conn;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _connStr;


        #region ctr

        public Traceability(ConnectionStringModel connectionStringModel)
        {
            _connStr = connectionStringModel.ConnectionString;
        }

        public Traceability(string connectionString)
        {
            _connStr = connectionString;
        }

        #endregion








    }
}
