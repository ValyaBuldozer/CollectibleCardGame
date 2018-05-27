using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseNetworkArchitecture.Common;
using GameData.Models;
using Server.Network.Controllers;
using Server.Unity;

namespace Server.Controllers
{
    public class ConsoleMenu
    {
        public void Start()
        {
            bool conFlag = true;
            while (conFlag)
            {
                Console.WriteLine("Enter command");

                try
                {
                    var command = Console.ReadLine()?.ToLower();
                    var commandSplit = command?.Split(' ');

                    switch (commandSplit[0])
                    {
                        case "start":
                            //Console.WriteLine("Server starts...");
                            UnityKernel.Get<ServerController>().Start(IPAddress.Any, 8800);
                            //Console.WriteLine("Success");
                            break;
                        case "stop":
                            UnityKernel.Get<ServerController>().Stop();
                            conFlag = false;
                            break;
                        case "settings":
                            if (commandSplit.Length < 2)
                            {
                                Console.WriteLine("error");
                                break;
                            }

                            var settings = UnityKernel.Get<GameSettings>();
                            switch (commandSplit[1])
                            {

                                case "starthandcount":
                                    if (int.TryParse(commandSplit[2], out int value)
                                        && settings.PlayerHandCardsMaxCount >= value)
                                        UnityKernel.Get<GameSettings>().StartHandCardsCount = value;
                                    else
                                        Console.WriteLine("error");
                                    break;

                                default:
                                    Console.WriteLine("Unknown command");
                                    break;
                            }
                            break;
                        case "help":
                            Console.WriteLine("Awailable commands:\n" +
                                              "start\n" +
                                              "stop\n" +
                                              "settings\n" +
                                              "   starthandcount");
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
