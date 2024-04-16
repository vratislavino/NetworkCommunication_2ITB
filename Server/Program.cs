

using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static TcpListener server;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "start")
                {
                    StartServer();
                } else if(cmd == "exit")
                {
                    break;
                } else
                {
                    Console.WriteLine("Neznámý příkaz");
                }
            }
        }

        private async static Task StartServer()
        {
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            await Console.Out.WriteLineAsync("Server spuštěn..");
            try
            {
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    HandleCommunication(client);
                }
            } catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
        }

        private async static void HandleCommunication(TcpClient client)
        {
            using(var stream = client.GetStream())
            using(var reader = new StreamReader(stream))
            using(var writer = new StreamWriter(stream))
            {
                writer.AutoFlush = true;
                while(true)
                {
                    try
                    {
                        string message = await reader.ReadLineAsync();
                        if (message == null) break;
                        await Console.Out.WriteLineAsync(message);
                    } catch (Exception e)
                    {
                        await Console.Out.WriteLineAsync(e.Message);
                        break;
                    }
                }
            }
        }
    }
}
