using System;
using System.Collections.Generic;
using System.Linq;
using GameData.Enums;
using GameData.Models;
using GameData.Models.Action;
using GameData.Models.Cards;
using GameData.Models.Repository;

namespace GameData.Tests.TestData
{
    internal class TestGameActionRepository : GameActionRepository
    {
        public TestGameActionRepository()
        {
            Collection = new List<GameAction>
            {
                new GameAction("DamageAllFriendlyUnits", 1, "test", ActionParameterType.Damage,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits) iUnit.State.RecieveDamage(parameter);
                    }),
                new GameAction("BuffDamageSpellCards", 2, "test", ActionParameterType.Buff,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iCard in player.HandCards)
                        {
                            var cCard = iCard as SpellCard;
                            if (cCard?.ActionInfo.ParameterType == ActionParameterType.Damage)
                                cCard.ActionInfo.ParameterValue += parameter;
                        }
                    }),
                new GameAction("DamageAllEnemyUnits", 3, "test", ActionParameterType.Damage,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        var enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        foreach (var iUnit in enemyPlayer.TableUnits) iUnit.State.RecieveDamage(parameter);
                    }),
                new GameAction("HealAllFriendlyUnits", 4, "test", ActionParameterType.Heal,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits) iUnit.State.Heal(parameter);
                    }),
                new GameAction("DamageAllUnits", 5, "test", ActionParameterType.Damage,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits) iUnit.State.RecieveDamage(parameter);

                        var enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits) iUnit.State.RecieveDamage(parameter);
                    }),
                new GameAction("BuffAttackFriendlyUnits", 6, "test", ActionParameterType.Buff,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits) iUnit.State.Attack += parameter;
                    }),
                new GameAction("FullBuffFriendlyUnits", 7, "test", ActionParameterType.Buff,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.Attack += parameter;
                            iUnit.State.BaseHealth += parameter;
                        }
                    }),
                new GameAction("DamageRandomEnemyUnit", 8, "test", ActionParameterType.Damage,
                    (controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        var enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        if (enemyPlayer.TableUnits.Count != 0)
                        {
                            var rnd = new Random();
                            var rndNum = rnd.Next(1, enemyPlayer.TableUnits.Count + 1);
                            enemyPlayer.TableUnits[rndNum].State.RecieveDamage(parameter);
                        }
                    })
            };
        }
    }
}