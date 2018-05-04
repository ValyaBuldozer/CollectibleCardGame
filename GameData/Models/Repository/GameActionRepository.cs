using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Models.Repository
{
    public class GameActionRepository
    {
        public List<GameAction> Collection { get; set; }

        public GameActionRepository()
        {
            Collection = new List<GameAction>()
            { new GameAction(name: "DamageAllFriendlyUnits",id: 1,description:"Нанесение урона по всем дружественным юнитам",parameterType: ActionParameterType.Damage,
                    action:((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }
                    })),
                new GameAction(name: "BuffDamageSpellCards",id: 2,description:"Увеличение урона атакуюших заклинаний, находящихся в руке",parameterType: ActionParameterType.Buff,
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
                new GameAction(name:"DamageAllEnemyUnits",id:3,description:"Нанесение урона всем вражеским юнитам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        foreach (var iUnit in enemyPlayer.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"HealAllFriendlyUnits",id:4,description:"Восстановление здоровья всех дружественных юнитов",parameterType:ActionParameterType.Heal,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.HealthPoint.Heal(parameter);
                        }

                    })),
                new GameAction(name:"DamageAllUnits",id:5,description:"Нанесение урона всем юнитам на столе",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits)
                        {
                            iUnit.HealthPoint.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"BuffAttackFriendlyUnits",id:6,description:"Повышение атаки всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.Attack += parameter;
                        }

                    })),
                new GameAction(name:"FullBuffFriendlyUnits",id:7,description:"Повышение атаки и здоровья всем дружественным юнитам",parameterType:ActionParameterType.Buff,
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
                new GameAction(name:"DamageRandomEnemyUnit",id:8,description:"Нанесение урона случайному вражескому юниту",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        if (enemyPlayer.TableUnits.Count != 0)
                        {
                            var rnd = new Random();
                            int rndNum = rnd.Next(1, enemyPlayer.TableUnits.Count + 1);
                            enemyPlayer.TableUnits[rndNum].HealthPoint.RecieveDamage(parameter);
                        }



                    })),
                new GameAction(name:"DamageEnemyFortress",id:9,description:"Нанесение урона вражеской крепости (герою)",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        enemyPlayer.HeroUnit.HealthPoint.RecieveDamage(parameter);



                    })),
                new GameAction(name:"SelfIncreaseAttack",id:10,description:"Юнит увеличивает свой показатель атаки",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var unit = (Unit) sender;
                        unit.Attack += parameter;
                        



                    })),
                new GameAction(name:"SelfIncreaseHealth",id:11,description:"Юнит увеличивает свой показатель здоровья",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var unit = (Unit) sender;
                        unit.HealthPoint.Heal(parameter);



                    })),

            };
        }
    }
}