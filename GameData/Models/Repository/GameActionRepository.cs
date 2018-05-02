using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Table;
using GameData.Enums;
using GameData.Models.Action;
using GameData.Models.Units;
using GameData.Models.Cards;

namespace GameData.Models.Repository
{
    public class GameActionRepository
    {
        public List<GameAction> Collection { get; set; }

        public GameActionRepository()
        {
            Collection = new List<GameAction>()
            {

                        enemyPlayer.TableUnits.ForEach(u=>u.HealthPoint.RecieveDamage(parameter));
                    }
                }
            };
        }
    }
}
