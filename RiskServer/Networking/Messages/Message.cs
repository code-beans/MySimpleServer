using System;
using Newtonsoft.Json;

namespace ConsoleApplication1.Networking.Messages {
	public class Message : IMessage {

		[JsonProperty(PropertyName = "Class")]
		public string MessageClass { get; set; }

		public Guid Token { get; private set; }

		public Message(Guid token,string messageClass) {
			MessageClass = messageClass;
			Token = token;
		}

		public override string ToString() {
			return JsonConvert.SerializeObject(this);
		}
	}
}
