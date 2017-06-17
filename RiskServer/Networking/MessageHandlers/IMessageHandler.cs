using Newtonsoft.Json.Linq;

namespace ConsoleApplication1.Networking.MessageHandlers {
	public interface IMessageHandler {


		string HandlingClass { get; }
		/// <summary>
		/// Handles a Message. Returns true if consumed, false otherwise
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		void Handle(JObject message);
	}
}
