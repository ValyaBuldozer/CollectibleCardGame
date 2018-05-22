using System;
using System.Linq;
using System.Net;
using BaseNetworkArchitecture.Common;
using Server.Network.Controllers;
using Server.Repositories;
using Server.Unity;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing Kernel...");
            UnityKernel.InitializeKernel();

            //пробуем подключится к бд
            var databaseRepos = UnityKernel.Get<UserRepository>();

            int count;
            if (databaseRepos.IsDatabaseConnected)
                //подгружаем для скороости работы
                count = databaseRepos.DatabaseCollection.Count();

            var conFlag = true;
            while (conFlag)
            {
                Console.WriteLine("Enter command");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "start":
                            //Console.WriteLine("Server starts...");
                            UnityKernel.Get<ServerController>().Start(IPAddress.Any, 8800);
                            //Console.WriteLine("Succes");
                            break;
                        case "stop":
                            UnityKernel.Get<ServerController>().Stop();
                            conFlag = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    UnityKernel.Get<ILogger>().LogAndPrint(e.Message);
                }
            }
        }
    }
}