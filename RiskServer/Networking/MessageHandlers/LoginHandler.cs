using System;
using System.Collections.Generic;
using ConsoleApplication1.DB;
using ConsoleApplication1.Networking.Messages.In;
using ConsoleApplication1.Networking.Messages.Out;

namespace ConsoleApplication1.Networking.MessageHandlers {
	public class LoginHandler : MessageHandler<LoginMessage> {
		private readonly IMessageExitPoint _exitPoint;

		private readonly Dictionary<string,Guid> _loggedInUsers = new Dictionary<string, Guid>();
		public LoginHandler(IMessageExitPoint exitPoint) : base("login") {
			_exitPoint = exitPoint;
		}

		public override void Handle(LoginMessage message) {
			if (message == null)
				throw new ArgumentNullException(nameof(message));
			var player = Player.GetByUsername(message.Username);
			if (player == null) {
				_exitPoint.Send(new Errormessage(message.Token,"User with Username does not exists"));
				return;
			}
			if (player.Username != message.Username) {
				_exitPoint.Send(new Errormessage(message.Token,"Wrong password"));
				return;
			}
			
			if (_loggedInUsers.ContainsKey(message.Username)) {
				_loggedInUsers[message.Username] = message.Token;
			} else {
				_loggedInUsers.Add(message.Username,message.Token);
			}
			_exitPoint.Send(new LoginResponse(message.Token));
		}
	}
}
