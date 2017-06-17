using System.Linq;
using Dapper;

namespace ConsoleApplication1.DB {
	public class Player {
		
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }


		public static int Insert(string username, string password) {
			using (var connection = ConnectionFactory.GetOpenConnection()) {
				var id = connection.Insert(new Player() {Username = username, Password = password});
				return (int) id;
			}
		}

		public static Player GetByUsername(string username) {
			using (var conn = ConnectionFactory.GetOpenConnection()) {
				return conn.Query<Player>("select * from players where players.Username = @Username", new {username}).First();
			}
		}

	}
}
