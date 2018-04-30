using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Cards;
using GameData.Models.Repository;

namespace GameData.Tests.TestData
{
    class TestGameActionRepository: GameActionRepository
    {
        public TestGameActionRepository()
        {
            Collection = new List<GameAction>()
            {
                new GameAction(name: "DamageAllFriendlyUnits",id: 1,description:"test",parameterType: ActionParameterType.Damage,
                    action:((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }
                    })),
                new GameAction(name: "BuffDamageSpellCards",id: 2,description:"test",parameterType: ActionParameterType.Buff,
                    action:((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iCard in player.HandCards)
                        {
                            var cCard = iCard as SpellCard;
                            if (cCard?.ActionInfo.ParameterType == ActionParameterType.Damage)
                                cCard.ActionInfo.ParameterValue+=parameter;
                        }
                    })),
                new GameAction(name:"DamageAllEnemyUnits",id:3,description:"test",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer=null;
                        foreach (var pplayer in controller.GetTableCondition.Players)
                        {
                            if (!Equals(pplayer, player))
                                 enemyPlayer = pplayer;

                        }
                        foreach (var iUnit in enemyPlayer.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"HealAllFriendlyUnits",id:4,description:"test",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.HealthPoint.Heal(parameter);
                        }

                    })),
                new GameAction(name:"DamageAllUnits",id:5,description:"test",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }

                        Player enemyPlayer=null;
                        foreach (var pplayer in controller.GetTableCondition.Players)
                        {
                            if (!Equals(pplayer, player))
                                enemyPlayer = pplayer;

                        }
                        foreach (var iUnit in enemyPlayer.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"BuffAttackFriendlyUnits",id:6,description:"test",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.Attack += parameter;
                        }

                    })),
                new GameAction(name:"FullBuffFriendlyUnits",id:7,description:"test",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.Attack += parameter;
                            iUnit.HealthPoint.Base += parameter;
                        }

                    })),
                new GameAction(name:"DamageRandomEnemyUnit",id:8,description:"test",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                       
                        var player = (Player) sender;
                        Player enemyPlayer=null;
                        foreach (var pplayer in controller.GetTableCondition.Players)
                        {
                            if (!Equals(pplayer, player))
                                enemyPlayer = pplayer;

                        }

                        if (enemyPlayer.TableUnits.Count != 0)
                        {
                            var rnd = new Random();
                            int rndNum = rnd.Next(1, enemyPlayer.TableUnits.Count + 1);
                            enemyPlayer.TableUnits[rndNum].HealthPoint.RecieveDamage(parameter);
                        }
                        


                    })),

            };
        }
    }
}
