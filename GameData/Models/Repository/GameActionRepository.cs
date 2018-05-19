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
                        if(!(sender is Player player)) return;

                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }
                    })),
                new GameAction(name: "BuffDamageSpellCards",id: 2,description:"Увеличение урона атакуюших заклинаний, находящихся в руке",parameterType: ActionParameterType.Buff,
                    action:((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
                        
                        foreach (var iCard in player.HandCards)
                        {
                            var cCard = iCard as SpellCard;
                            if (cCard?.ActionInfo.ParameterType == ActionParameterType.Damage)
                                cCard.ActionInfo.ParameterValue += parameter;
                        }
                    })),
                new GameAction(name:"DamageAllEnemyUnits",id:3,description:"Нанесение урона всем вражеским юнитам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
						
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        foreach (var iUnit in enemyPlayer.TableUnits)
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"HealAllFriendlyUnits",id:4,description:"Восстановление здоровья всех дружественных юнитов",parameterType:ActionParameterType.Heal,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;

                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.Heal(parameter);
                        }

                    })),
                new GameAction(name:"DamageAllUnits",id:5,description:"Нанесение урона всем юнитам на столе",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
						
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits)
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }

                    })),
                new GameAction(name:"BuffAttackFriendlyUnits",id:6,description:"Повышение атаки всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
						
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.Attack += parameter;
                        }
                    })),
                new GameAction(name:"FullBuffFriendlyUnits",id:7,description:"Повышение атаки и здоровья всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
						
                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.Attack += parameter;
                            iUnit.State.BaseHealth += parameter;
                        }

                    })),
                new GameAction(name:"DamageRandomEnemyUnit",id:8,description:"Нанесение урона случайному вражескому юниту",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
						
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(
                                p => p.Username != player.Username);

                        if (enemyPlayer.TableUnits.Count != 0)
                        {
                            var rnd = new Random();
                            int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count);
                            enemyPlayer.TableUnits[rndNum].State.RecieveDamage(parameter);
                        }
                    })),
                new GameAction(name:"DamageEnemyFortress",id:9,description:"Нанесение урона вражеской крепости (герою)",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
						
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(
                                p => p.Username != player.Username);

                        enemyPlayer.HeroUnit.State.RecieveDamage(parameter);



                    })),
                new GameAction(name:"SelfIncreaseAttack",id:10,description:"Юнит увеличивает свой показатель атаки",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Unit unit)) return;
						
                        unit.State.Attack += parameter;
                    })),
                new GameAction(name:"SelfIncreaseHealth",id:11,description:"Юнит восстанавливает здоровье",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Unit unit)) return;
						
                        unit.State.Heal(parameter);
                    })),
                #endregion

                #region Мажорные

                #region Параметровые
                
                new GameAction(name:"Полное усиление",id:20,description:"Повышение атаки и здоровья всем дружественным юнитам",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        Player player = sender is Player ? (Player) sender : (sender as Unit).Player;

                        foreach (var iUnit in player.TableUnits)
                        {
                            iUnit.State.Attack += parameter;
                            iUnit.State.BaseHealth += parameter;
                        }
                    })),
                new GameAction(name:"Повышение атаки",id:21,description:"Юнит повышает показатель атаки",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Unit unit)) return;
						
                        unit.State.Attack += parameter;
                    })),
                new GameAction(name:"Нанесение урона герою",id:22,description:"Наносит урон герою противника",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    
                        {Player player = sender is Player ? (Player) sender : (sender as Unit).Player;

                        
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        enemyPlayer.HeroUnit.State.RecieveDamage(parameter);
                    })),
                  //(не параметровый)
                new GameAction(name:"Полное выздоровление",id:23,description:"Восстанавливает здоровье выбранного союзного юнита до максимума",parameterType:ActionParameterType.Heal,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Unit unit)) return;

                       
                        target.State.Heal(target.State.BaseHealth-target.State.GetResultHealth);
                    })),
                new GameAction(name:"Выдача карт(ы)",id:24,description:"Выдает карту(ы) из колоды игрока",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        Player player = sender is Player ? (Player) sender : (sender as Unit).Player;

                        controller.DrawCard(player,parameter);
                    })),
                new GameAction(name:"Урон по юниту",id:25,description:"Нанесение урона выбранному юниту",parameterType:ActionParameterType.Damage,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                       
                       target.State.RecieveDamage(parameter); 
                    })),
                new GameAction(name:"Улучшение заклинаний",id:26,description:"Повышение силы атакующих заклинаний в руке",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Unit unit)) return;
                        foreach (var iCard in unit.Player.HandCards.ToArray())
                        {
                            var cCard = iCard as SpellCard;
                            if (cCard?.ActionInfo.ParameterType == ActionParameterType.Damage)
                                cCard.ActionInfo.ParameterValue+=parameter;
                        }


                    })),
                    //(не параметровый)
                new GameAction(name:"Случайное замораживание",id:27,description:"Замораживает случайного вражеского юнита на 1 ход",parameterType:ActionParameterType.Empty,
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
                new GameAction(name:"Восстановление здоровья",id:28,description:"Восстановление здоровья выбранному юниту",parameterType:ActionParameterType.Heal,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                       
                        target.State.Heal(parameter);


                    })),
                new GameAction(name:"Всеобщее восстановление",id:29,description:"Восстановление здоровья всем дружественным юнитам",parameterType:ActionParameterType.Heal,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Unit unit)) return;
                       

                        foreach (var iUnit in unit.Player.TableUnits)
                        {
                            iUnit.State.Heal(parameter);
                        }


                    })),
                //(не параметровый)
                new GameAction(name:"Врыв",id:30,description:"+1/+1 за каждый союзный юнит на столе",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {

                        if(!(sender is Unit unit)) return;
                        
                        unit.State.Attack+=unit.Player.TableUnits.Count;
                        unit.State.BaseHealth += unit.Player.TableUnits.Count;


                    })),
                //(не параметровый)
                new GameAction(name:"Выход из режима маскировки",id:31,description:"Маскировка пропадает",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {

                        if(!(sender is Unit unit)) return;

                        unit.State.AttackPriority = 1;


                    })),
                //(не параметровый)
                new GameAction(name:"Выдача карты при атаке",id:32,description:"Выдача карты из колоды игрока при атаке юнита с такой способностью",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {

                        if(!(sender is Unit unit)) return;

                        controller.DrawCard(unit.Player,1);


                    })),


               
                
                //по сути для юнитов
                //////////////////////////////////////////////////////
                //по сути для спеллов
               
                new GameAction(name:"Урон всем вражеским отрядам",id:40,description:"Наносит урон всем вражеским отрядам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(parameter);
                        }


                    })),
                //(не параметровый)
                new GameAction(name:"Выдача золота",id:41,description:"Выдает дополнительный золотой игроку на текущий ход",parameterType:ActionParameterType.Buff,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
                        if(player.Mana.Current<10)
                        player.Mana.Current = player.Mana.Base+1;
                    })),
                new GameAction(name:"Мягкая сталь",id:42,description:"Понижает атаку вражеского юнита",parameterType:ActionParameterType.Buff,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                        target.State.Attack-=parameter;



                    })),
                new GameAction(name:"Внезапное усиление",id:43,description:"Повышение атаки и здоровья выбранному юниту",parameterType:ActionParameterType.BuffRandomRange,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {

                        target.State.BaseHealth+=parameter;
                        target.State.Attack += parameter;

                    })),
                new GameAction(name:"Заточенные клинки",id:44,description:"Повышение атаки выбранному юниту",parameterType:ActionParameterType.Buff,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.Attack += parameter;

                    })),
                new GameAction(name:"Прилив сил",id:45,description:"Повышение здоровья выбранному юниту",parameterType:ActionParameterType.Buff,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.BaseHealth += parameter;

                    })),
                new GameAction(name:"Урон всем",id:46,description:"Наносит урон всем юнитам на столе",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
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
                new GameAction(name:"Энергетический выброс",id:47,description:"Случайно распределяет урон по вражеским юнитам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
                        var enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        var rnd = new Random();


                        for (var i = 0; i <= parameter; i++)
                            if (enemyPlayer.TableUnits.Count != 0)
                            {
                                var rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count);
                                enemyPlayer.TableUnits[rndNum].State.RecieveDamage(1);
                            }
                    })),

                #endregion

                #region Независимые
               
                new GameAction(name:"Пробивающий разряд",id:50,description:"Наносит 3 урона герою противника и 1 урон всем вражеским юнитам",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        enemyPlayer.HeroUnit.State.RecieveDamage(3);
                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.RecieveDamage(1);
                        }



                    })),
                new GameAction(name:"Сфера поглощения",id:51,
                    description:"Высасывает 1 единицу здоровья у всех юнитов противника и повышает всем союзным юнитам 1 единицу здоровья",
                    parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
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
   
                new GameAction(name:"Техника клонирования",id:52,
                    description:"При выборе какого-либо юнита, его карта разыгрвается на стороне игрока (копируется)",
                    parameterType:ActionParameterType.Empty,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        var cloneCard = target.BaseCard;
                        

                    })),
                new GameAction(name:"Подкуп",id:53,
                    description:"Выбранная карта уничтожается, а её копия разыгрывается на стороне игрока (переходит на сторону игрока)",parameterType:ActionParameterType.Empty,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        


                    })),
                new GameAction(name:"Тактическое отступление",id:54,description:"Выбранная карта уходит в руку к игроку, на ее месте разыгрывается карта Чучела (провокатор 0/2)",parameterType:ActionParameterType.Empty,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        


                    })),
                new GameAction(name:"Всеобщее отступление",id:55,description:"Все карты игрока возвращаются к нему в руку",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        //todo: DrawCard для опредленного юнита
                        


                    })),
                new GameAction(name:"Казнь",id:56,description:"Уничтожает рандомный юнит противника",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        if(!(sender is Player player)) return;
                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        var rnd = new Random();
                        var rndUnit = enemyPlayer.TableUnits[rnd.Next(0, enemyPlayer.TableUnits.Count + 1)];
                        controller.KillUnit(rndUnit);
                    })),
                new GameAction(name:"Крещение огнем",id:57,description:"Здоровье выбранного юнита понижается на 2, атака повышается на 4",parameterType:ActionParameterType.Buff,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {


                      
                        target.State.Attack += 4;
                        target.State.BaseHealth -= 2;

                    })),
                new GameAction(name:"Живой щит",id:58,description:"Выбранный союзный юнит становится провокатором и его здоровье повышается на 2",parameterType:ActionParameterType.Buff,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {


                        target.State.BaseHealth += 2;
                        target.State.AttackPriority = 2;
                       

                    })),
                new GameAction(name:"Деморализация",id:59,description:"Понижает атаку и здоровье выбранного юнита до 0/2 и наделяет способностью провокация",parameterType:ActionParameterType.Empty,
                    isTargeted:true,
                    action: ((controller, sender, target, parameter) =>
                    {

                        target.State.Attack = 0;
                        target.State.BaseHealth = 2;
                        target.State.AttackPriority = 2;
                        

                    })),
                new GameAction(name:"Последний призыв",id:60,description:"Унчтожает все карты на столе",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        if(!(sender is Player player)) return;
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
                new GameAction(name:"Резня",id:61,description:"Уничтожает все отряды противника кроме одного случайного",parameterType:ActionParameterType.Damage,
                    action: ((controller, sender, target, parameter) =>
                    {

                        if(!(sender is Player player)) return;

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);
                        if (enemyPlayer.TableUnits.Count>1)
                        {
                            var rnd = new Random();
                            int rndNum = rnd.Next(0, enemyPlayer.TableUnits.Count);
                            var unit = enemyPlayer.TableUnits[rndNum];

                            foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                            {
                                if (!Equals(iUnit,unit)) controller.KillUnit(iUnit);
                            }

                           
                           
                        }

                    })),
                new GameAction(name:"Обледенение",id:62,description:"Все юниты на столе замораживаются на 1 ход",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {

                        //todo: сделать заморозку юнита

                    })),
                new GameAction(name:"Метель",id:63,description:"Снижает атаку отрядов до 1 у обоих игроков",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
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
                new GameAction(name:"Обледенение",id:64,description:" Снижает здоровье отрядов до 1 у обоих игроков",parameterType:ActionParameterType.Empty,
                    action: ((controller, sender, target, parameter) =>
                    {
                        if(!(sender is Player player)) return;
                        foreach (var iUnit in player.TableUnits.ToArray())
                        {
                            iUnit.State.BaseHealth = 1;
                        }

                        Player enemyPlayer =
                            controller.GetTableCondition.Players.FirstOrDefault(p => p.Username != player.Username);

                        foreach (var iUnit in enemyPlayer.TableUnits.ToArray())
                        {
                            iUnit.State.BaseHealth = 1;
                        }


                    })),
               
                
               
                #endregion
                #endregion

            };
        }
    }
}
