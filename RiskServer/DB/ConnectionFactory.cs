using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1.DB
{
    public class ConnectionFactory {

		private static readonly string ConnectionString;
		static ConnectionFactory() {
			var builder = new MySqlConnectionStringBuilder {
				UserID = "root",
				Password = "admin",
				Server = "127.0.0.1",
				Port = 3306,
				Database = "riskdb"
			};

			ConnectionString = builder.ToString();
			Console.WriteLine(ConnectionString);

		}
		public static DbConnection GetOpenConnection() {

			var connection = new MySqlConnection(ConnectionString);
			connection.Open();

			return connection;
		}
	}
}