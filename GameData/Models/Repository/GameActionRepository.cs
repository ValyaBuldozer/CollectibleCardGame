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
            {
                #region Минорные
                new GameAction(name: "DamageAllFriendlyUnits",id: 1,description:"Нанесение урона по всем дружественным юнитам",parameterType: ActionParameterType.Damage,
                    action:((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }
                    })),
                new GameAction(name: "BuffDamageSpellCards",id: 2,description:"Увеличение урона атакуюших заклинаний, находящихся в руке",parameterType: ActionParameterType.Buff,
                    action:((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iCard in player.HandCards.ToArray())
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
                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"HealAllFriendlyUnits",id:4,description:"Восстановление здоровья всех дружественных юнитов",parameterType:ActionParameterType.Heal,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.Heal(parameter);
                        }

                    })),
                new GameAction(name:"DamageAllUnits",id:5,description:"Нанесение урона всем юнитам на столе",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"BuffAttackFriendlyUnits",id:6,description:"Повышение атаки всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.Attack += parameter;
                        }

                    })),
                new GameAction(name:"FullBuffFriendlyUnits",id:7,description:"Повышение атаки и здоровья всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        //controller.DrawCard((Player)sender);
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.Attack += parameter;
                            iUnit.State.BaseHealth += parameter;
                        }

                    })),
                new GameAction(name:"DamageRandomEnemyUnit",id:8,description:"Нанесение урона случайному вражескому юниту",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(
                                p => p.Username != player.Username);

                        if (enemyPlayer.TableUnits.Count != 0)
                        {
                            var rnd = new Random();
                            int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count + 1);
                            enemyPlayer.TableUnits[rndNum].State.RecieveDamage(parameter);
                        }



                    })),
                new GameAction(name:"DamageEnemyFortress",id:9,description:"Нанесение урона вражеской крепости (герою)",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(
                                p => p.Username != player.Username);

                        enemyPlayer.HeroUnit.State.RecieveDamage(parameter);



                    })),
                new GameAction(name:"SelfIncreaseAttack",id:10,description:"Юнит увеличивает свой показатель атаки",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var unit = (Unit) sender;
                        unit.State.Attack += parameter;
                        



                    })),
                new GameAction(name:"SelfIncreaseHealth",id:11,description:"Юнит восстанавливает здоровье",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var unit = (Unit) sender;
                        unit.State.Heal(parameter);



                    })),
                #endregion

                #region Мажорные

                #region Параметровые
                
                new GameAction(name:"Полное усиление",id:20,description:"Повышение атаки и здоровья всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.Attack += parameter;
                            iUnit.State.BaseHealth += parameter;
                        }



                    })),
                new GameAction(name:"Повышение атаки",id:21,description:"Юнит повышает показатель атаки",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var unit = (Unit) sender;
                        unit.State.Attack += parameter;



                    })),
                new GameAction(name:"Нанесение урона герою",id:22,description:"Наносит урон герою противника",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        enemyPlayer.HeroUnit.State.RecieveDamage(parameter);



                    })),
                new GameAction(name:"Полное выздоровление",id:23,description:"Восстанавливает здоровье выбранного союзного юнита до максимума",parameterType:ActionParameterType.Heal,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        if (Equals(target.Player, player))
                        target.State.Heal(target.State.BaseHealth-target.State.GetResultHealth);



                    })),
                new GameAction(name:"Выдача карт(ы)",id:24,description:"Выдает карту(ы) из колоды игрока",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        controller.DrawCard(player,parameter);



                    })),
                new GameAction(name:"Урон по юниту",id:25,description:"Нанесение урона выбранному юниту",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                       target.State.RecieveDamage(parameter);
                        

                    })),
                new GameAction(name:"Улучшение заклинаний",id:26,description:"Повышение силы атакующих заклинаний в руке",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iCard in player.HandCards.ToArray())
                        {
                            var cCard = iCard as SpellCard;
                            if (cCard?.ActionInfo.ParameterType == ActionParameterType.Damage)
                                cCard.ActionInfo.ParameterValue+=parameter;
                        }


                    })),
                new GameAction(name:"Случайное замораживание",id:27,description:"Замораживает случайного вражеского юнита на 1 ход",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //var player = (Player) sender;
                        //Player enemyPlayer =
                        //    controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        //if (enemyPlayer.TableUnits.Count != 0)
                        //{
                        //    var rnd = new Random();
                        //    int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count + 1);
                        //    enemyPlayer.TableUnits[rndNum].CanAttack=false;
                        //}

                        //todo: сделать заморозку юнита


                    })),
                new GameAction(name:"Восстановление здоровье",id:28,description:"Восстановление здоровья выбранному юниту",parameterType:ActionParameterType.Heal,
                    action: ((controller, sender, target, parameter) =>
                    {

                        target.State.Heal(parameter);


                    })),
               
                
                //как бы для юнитов
                //////////////////////////////////////////////////////
                //как бы для спеллов
               
                new GameAction(name:"Урон всем вражеским отрядам",id:40,description:"Наносит урон всем вражеским отрядам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }


                    })),
                new GameAction(name:"Выдача золота",id:41,description:"Выдает дополнительное золото игроку",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                       //todo: Выдача золота игроку



                    })),
                new GameAction(name:"Мягкая сталь",id:42,description:"Понижает атаку вражеского юнита",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        target.State.Attack-=parameter;



                    })),
                new GameAction(name:"Внезапное усиление",id:43,description:"Повышение атаки и здоровья выбранному юниту",parameterType:ActionParameterType.BuffRandomRange,
                    action: ((controller, sender, target, parameter) =>
                    {

                        target.State.BaseHealth+=parameter;
                        target.State.Attack += parameter;

                    })),
                new GameAction(name:"Заточенные клинки",id:44,description:"Повышение атаки выбранному юниту",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.Attack += parameter;

                    })),
                new GameAction(name:"Прилив сил",id:45,description:"Повышение здоровья выбранному юниту",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.BaseHealth += parameter;

                    })),

                #endregion

                #region Независимые
                new GameAction(name:"Удар с неба",id:50,description:"Случайно распределяет урон по вражеским юнитам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        var rnd = new Random();
                        if (enemyPlayer.TableUnits.Count != 0)
                        {
                            for (int i = 0; i <= parameter; i++)
                            {

                                int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count + 1);
                                enemyPlayer.TableUnits[rndNum].State.RecieveDamage(1);
                            }
                        }



                    })),
                new GameAction(name:"Пробивающий разряд",id:51,description:"Наносит 3 урона герою противника и 1 урон всем вражеским юнитам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        enemyPlayer.HeroUnit.State.RecieveDamage(3);
                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(1);
                        }



                    })),
                new GameAction(name:"Сфера поглощения",id:52,description:"Высасывает 1 единицу здоровья у всех юнитов противника и повышает всем союзным юнитам 1 единицу здоровья",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.BaseHealth += 1;
                        }
                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(1);
                        }

                    })),
                new GameAction(name:"Проклятье темной звезды",id:53,description:"Наносит всем юнитам на столе 4 урона",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"Техника клонирования",id:54,description:"При выборе какого-либо юнита, его карта разыгрвается на стороне игрока (копируется)",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        var cloneCard = target.BaseCard;
                        

                    })),
                new GameAction(name:"Подкуп",id:55,description:"Выбранная карта уничтожается, а её копия разыгрывается на стороне игрока (переходит на сторону игрока)",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        


                    })),
                new GameAction(name:"Тактическое отступление",id:56,description:"Выбранная карта уходит в руку к игроку, на ее месте разыгрывается карта Чучела (провокатор 0/2)",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        


                    })),
                new GameAction(name:"Всеобщее отступление",id:57,description:"Все карты игрока возвращаются к нему в руку",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        


                    })),
                new GameAction(name:"Казнь",id:58,description:"Уничтожает рандомный юнит противника",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        var rnd = new Random();
                        int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count + 1);
                        controller.KillUnit(enemyPlayer.TableUnits[rndNum]);


                    })),
                new GameAction(name:"Крещение огнем",id:59,description:"Здоровье выбранного юнита понижается на 2, атака повышается на 4",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.BaseHealth -= 2;
                        target.State.Attack += 4;

                    })),
                new GameAction(name:"Живой щит",id:60,description:"Выбранный союзный юнит становится провокатором и его здоровье повышается на 2",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.BaseHealth += 2;
                        //todo: сделать юнит провокатором 

                    })),
                new GameAction(name:"Деморализация",id:61,description:"Понижает атаку и здоровье выбранного юнита до 0/2 и наделяет способностью провокация",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {

                        target.State.Attack = 0;
                        target.State.BaseHealth = 2;
                        //todo: сделать юнит провокатором 

                    })),
                new GameAction(name:"Последнйи призыв",id:62,description:"Унчтожает все карты на столе",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            controller.KillUnit(iUnit);
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            controller.KillUnit(iUnit);
                        }

                    })),
                new GameAction(name:"Резня",id:63,description:"Уничтожает все отряды противника кроме одного случайного",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        var player = (Player) sender;
                       
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        if (enemyPlayer.TableUnits.Count>1)
                        {
                            var rnd = new Random();
                            int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count + 1);

                            for (int i = 0; i <=enemyPlayer.TableUnits.Count ; i++)
                            {
                                if (i!=rndNum)
                                    controller.KillUnit(enemyPlayer.TableUnits[i]);
                            }
                           
                        }

                    })),
                new GameAction(name:"Обледенение",id:64,description:"Все юниты на столе замораживаются на 1 ход",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {

                        //todo: сделать заморозку юнита

                    })),
                new GameAction(name:"Метель",id:65,description:"Снижает атаку отрядов до 1 у обоих игроков",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        var player = (Player) sender;
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.Attack = 1;
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.Attack = 1;
                        }


                    })),
               
                #endregion
                #endregion

            };
        }
    }
}