using Nancy.Hosting.Self;
using System;

namespace Demo_Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new NancyHost(new Uri("http://localhost:4000"));

            host.Start();
            Console.WriteLine("Server Iniciado...");
            Console.WriteLine("Pressione uma tecla para encerrar");
            Console.ReadKey();

            host.Stop();
        }
    }
}
