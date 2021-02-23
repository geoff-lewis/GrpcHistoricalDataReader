using Grpc.Core;
using System;

namespace HistoricalDataServer
{
    public class Program
    {
    const int Port = 30051;

    public static void Main(string[] args)
    {
      Server server = new Server
      {
        Services = { Grpc.HistoricalData.HistoricalDataService.BindService(new HistoricalDataServiceImplementation()) },
        Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
      };
      server.Start();

      Console.WriteLine("Greeter server listening on port " + Port);
      Console.WriteLine("Press any key to stop the server...");
      Console.ReadKey();

      server.ShutdownAsync().Wait();
    }
  }
}
