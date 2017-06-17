using System.Threading.Tasks;
using ConsoleApplication1.Networking.Messages;

namespace ConsoleApplication1.Networking {
	/// <summary>
	/// Exit point for sending messages 
	/// </summary>
	public interface IMessageExitPoint {
		/// <summary>
		/// Sends message to adressee
		/// </summary>
		/// <param name="message"></param>
		void Send(IMessage message);

		/// <summary>
		/// Sends message to adresse. Response will include client response if any. FailureCallback will invoked after timeout or if message could not be sent.
		/// </summary>
		/// <param name="message">Message to send</param>
		/// <param name="timeout">optional timeout in ms</param>
		Task<IMessage> SendAsync(IMessage message, int? timeout= null);
	}
}
