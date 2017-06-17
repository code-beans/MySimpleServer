using System;

namespace ConsoleApplication1.Networking.Messages.Out {
	public class LoginResponse : Message {
		public LoginResponse(Guid token) : base(token,"login") {
		}
	}
}
