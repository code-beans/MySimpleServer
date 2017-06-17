using ConsoleApplication1.Networking.Messages;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1.Networking.MessageHandlers {
	public abstract class MessageHandler<T> : IMessageHandler where T : Message {
		public string HandlingClass { get; }

		protected MessageHandler(string handlingClass) {
			HandlingClass = handlingClass;
		}

		public abstract void Handle(T message);
		void IMessageHandler.Handle(JObject message) => Handle(message.ToObject<T>());
		
	}
}
