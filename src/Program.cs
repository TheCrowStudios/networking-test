using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";
            Console.Write("IP to connect to: ");
            Client client = new Client(Console.ReadLine());
            client.ConnectToServer();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
