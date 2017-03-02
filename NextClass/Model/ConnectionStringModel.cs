using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters;
using System.Text;
using NextClass.Extensions;
using static System.String;

namespace NextClass.Model
{
    public class ConnectionStringModel
    {
        private string _name;

        public string Name => !IsNullOrEmpty(_name) ? _name:"Default";
        public string ConnectionString { get; }

        public ConnectionStringModel()
        {
            throw new NotImplementedException();
        }
        public ConnectionStringModel(string connectionString)
        {
            if (IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));

            ConnectionString = GenerateConnectionString(connectionString);
        }
        public ConnectionStringModel(string name,string host, string catalog, string user, string password)
        {
            if (IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (IsNullOrWhiteSpace(host))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(host));
            if (IsNullOrWhiteSpace(catalog))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(catalog));
            if (IsNullOrWhiteSpace(user))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(user));
            if (IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            _name = name;
            ConnectionString = GenerateConnectionString(host, catalog, password, user);
        }
        public ConnectionStringModel(string host, string catalog, string user, string password)
        {
            if (IsNullOrWhiteSpace(host))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(host));
            if (IsNullOrWhiteSpace(catalog))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(catalog));
            if (IsNullOrWhiteSpace(user))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(user));
            if (IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            _name = Empty;
            ConnectionString = GenerateConnectionString(host, catalog, user, password);
        }

        private static string GenerateConnectionString(string host, string db,string user, string pass )
        {
            var sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = host,
                InitialCatalog = db,
                UserID = user             
            };

            sqlBuilder.Password = pass.IsEncrypted() ? sqlBuilder.Password.Decrypt() : pass;

            return sqlBuilder.ToString();
        }
        private static string GenerateConnectionString(string connectionString)
        {
            var sqlConnBuilder = new SqlConnectionStringBuilder(connectionString);

            if (sqlConnBuilder.Password.IsEncrypted())
            {
                sqlConnBuilder.Password = sqlConnBuilder.Password.Decrypt();

            }
            return sqlConnBuilder.ToString();
        }
    }
}
