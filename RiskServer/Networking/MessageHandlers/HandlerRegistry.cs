using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using ConsoleApplication1.Networking.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1.Networking.MessageHandlers {
	public class HandlerRegistry : IMessageEntryPoint, IMessageExitPoint {
		private readonly List<IMessageHandler> _handlers = new List<IMessageHandler>(); //instantiated handlers
		private readonly ConcurrentDictionary<string,IMessageHandler> _responsibleHandlers = new ConcurrentDictionary<string, IMessageHandler>(); //mapping
		
		private readonly HashSet<Guid> _knownTokens = new HashSet<Guid>();
		private readonly ConcurrentDictionary<Guid,Socket> _tokenToClients = new ConcurrentDictionary<Guid, Socket>();
		

		public void Subsribe(IMessageHandler messageHandler) => _handlers.Add(messageHandler);

		public void Handle(Socket client, string message) {
			var jobject = JObject.Parse(message);
			var tokenString = jobject.Value<string>("Token");

			var token = Guid.NewGuid();
			//no token present
			if (tokenString == null) {
				_knownTokens.Add(token);
				jobject.Add("Token",token);
				_tokenToClients.TryAdd(token, client);
			} else {
				//unknown token
				token = Guid.Parse(tokenString);
				if (!_knownTokens.Contains(token)) {
					token = Guid.NewGuid();
					_knownTokens.Add(token);
					jobject["Token"] = token;
					_tokenToClients.TryAdd(token, client);
				}
			}
			//route to proper handler
			var classString = jobject.Value<string>("Class");
			_responsibleHandlers.GetOrAdd(classString, klass => _handlers.Single(h => klass == h.HandlingClass)).Handle(jobject);
		}

		public void Send(IMessage message) => AsynchronousSocketListener.Send(_tokenToClients[message.Token],JsonConvert.SerializeObject(message));

		public Task<IMessage> SendAsync(IMessage message, int? timeout = null) {
			throw new NotImplementedException();
		}
	}

	public interface IMessageEntryPoint {
		void Handle(Socket client, string message);
	}
}
