using Dapper;

namespace ConsoleApplication1.DB {
	public static class Database {

		public static void Create() {
			var query =
				"CREATE TABLE IF NOT EXISTS `riskdb`.`player` (\n  `Id` INT NOT NULL AUTO_INCREMENT,\n  `Username` VARCHAR(45) NOT NULL,\n  `Password` VARCHAR(45) NOT NULL,\n  PRIMARY KEY (`Id`),\n  UNIQUE INDEX `Username_UNIQUE` (`Username` ASC));\n";

			using (var connection = ConnectionFactory.GetOpenConnection()) {
				connection.Execute(query);
			}
		}

		/// <summary>
		/// Danger Zone, drops the entire db
		/// </summary>
		public static void Drop() {
			
		}
	}
}
