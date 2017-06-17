using System;
using ConsoleApplication1.Networking;
using ConsoleApplication1.Networking.MessageHandlers;
using Microsoft.Extensions.DependencyInjection;
using RiskServer.Networking;

namespace RiskServer
{
    class Server {
		public static int Main(string[] args) {

            //setup DI
		    var serviceProvider = new ServiceCollection()
		        .AddSingleton<HandlerRegistry>()
                .AddSingleton<IMessageEntryPoint,HandlerRegistry>()
                .AddSingleton<IMessageExitPoint,HandlerRegistry>()
                .AddSingleton<LoginHandler>()
                .AddSingleton<NewPlayerHandler>()
		        .BuildServiceProvider();
			
			var registry = serviceProvider.GetService<HandlerRegistry>();
			var newPlayerHandler = serviceProvider.GetService<NewPlayerHandler>();
			registry.Subsribe(newPlayerHandler);
			var loginHandler = serviceProvider.GetService<LoginHandler>();
			registry.Subsribe(loginHandler);

			TcpHelper.StartServer(8000);

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
			return 0;
		}
	}
}
