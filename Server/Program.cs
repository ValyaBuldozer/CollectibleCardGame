using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using Server.Controllers;
using Server.Database;
using Server.Models;
using Server.Network.Controllers;
using Server.Repositories;
using Server.Unity;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Initializing Kernel...");
            UnityKernel.InitializeKernel();
            Console.WriteLine("Try to connect to database");

            //пробуем подключится к бд
            var databaseRepos = UnityKernel.Get<UserRepository>();

            int count;
            if (databaseRepos.IsDatabaseConnected)
                //подгружаем для скороости работы
            {
                count = databaseRepos.DatabaseCollection.Count();
                Console.WriteLine("Database connected");
            }

            ConsoleMenu menu = new ConsoleMenu();
            menu.Start();
        }
    }
}
