﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
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
            UnityKernel.Get<UserRepository>().Collection.Count();

            bool conFlag = true;
            while (conFlag)
            {
                Console.WriteLine("Enter command");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "start":
                            //Console.WriteLine("Server starts...");
                            UnityKernel.Get<ServerController>().Start(IPAddress.Parse("127.0.0.1"),8800);
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
