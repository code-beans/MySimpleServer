using System;

namespace ConsoleApplication1.Networking.Messages.In {
	public class NewPlayerMessage : Message {
		public string Username, Password;
	
		public NewPlayerMessage(string username, string password) : base(Guid.Empty, "newPlayer") {
			Username = username;
			Password = password;
		}
	}
}
