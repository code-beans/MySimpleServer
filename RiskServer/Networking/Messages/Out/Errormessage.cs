using System;
using Newtonsoft.Json;

namespace ConsoleApplication1.Networking.Messages.Out {
	public class Errormessage : Message {
		
		[JsonProperty("error")]
		public string ErrorMessage { get; set; }

		public Errormessage(Guid token, string errorMessage) : base(token,"error") {
			ErrorMessage = errorMessage;
		}
	}
}
