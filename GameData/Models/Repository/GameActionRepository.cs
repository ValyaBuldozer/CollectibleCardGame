using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Table;
using GameData.Enums;
using GameData.Models.Action;
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
                new GameAction()
                {
                    ID = 1,
                    Name = "TestFireball",
                    Description = "Deal n damage to target unit",
                    ParameterType = ActionParameterType.Damage,
                    Action = (controller, sender, target, parameter) =>
                    {
                        target.HealthPoint.RecieveDamage(parameter);
                    }
                },
                new GameAction()
                {
                    ID = 2,
                    Name = "TestAllUnits",
                    Description = "Deal n damage to all enemy units",
                    ParameterType = ActionParameterType.Damage,
                    Action = (controller, sender, target, parameter) =>
                    {
                        var enemyPlayer = controller.GetTableCondition.Players.FirstOrDefault(
                            p => !p.Equals((Player) sender));

                        enemyPlayer.TableUnits.ForEach(u=>u.HealthPoint.RecieveDamage(parameter));
                    }
                }
            };
        }
    }
}
