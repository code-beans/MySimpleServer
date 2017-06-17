using System;
using ConsoleApplication1.DB;
using ConsoleApplication1.Networking.Messages.In;
using ConsoleApplication1.Networking.Messages.Out;

namespace ConsoleApplication1.Networking.MessageHandlers {
	public class NewPlayerHandler : MessageHandler<NewPlayerMessage> {

		private readonly IMessageExitPoint _exitPoint;
		public NewPlayerHandler(IMessageExitPoint exitPoint) : base("newPlayer") {
			_exitPoint = exitPoint;
		}

		public override void Handle(NewPlayerMessage message) {
			if (message == null)
				throw new ArgumentNullException(nameof(message));
			var player = Player.GetByUsername(message.Username);

			if (player != null) {
				_exitPoint.Send(new Errormessage(message.Token, "Player with Username exists already"));
			}
			Player.Insert(message.Username, message.Password);
		}
	}
}