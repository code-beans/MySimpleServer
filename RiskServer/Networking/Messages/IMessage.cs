using System;

namespace ConsoleApplication1.Networking.Messages {
	/// <summary>
	/// Message class
	/// </summary>
	public interface IMessage {
		/// <summary>
		/// Message type
		/// </summary>
		string MessageClass { get; set; }

		
		/// <summary>
		/// Sender/Receiver based on context
		/// </summary>
		Guid Token { get; }
	}
}