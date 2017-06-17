using System;

namespace ConsoleApplication1.Networking.Messages.In {
	public class LoginMessage : Message {
		public string Username, Password;


		public LoginMessage(Guid token,string username, string password) : base(token,"login") {
			Username = username;
			Password = password;
		}
	}
}
