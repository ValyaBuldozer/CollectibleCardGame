using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Controllers.Table
{
    public interface IUnitDispatcher
    {
        bool SpawnRequest(UnitCard card, Player sender);
        void CardDeploySpawn(UnitCard card, Player sender);
        void Spawn(UnitCard card, Player sender);
        void Spawn(Unit unit, Player sender);
        void Kill(Unit unit);
        Unit GetUnit(UnitCard card);
    }

    public class UnitDispatcher : IUnitDispatcher
    {
        public bool SpawnRequest(UnitCard card, Player sender)
        {
            if (sender.TableUnits.Count >= 10)
                return false;

            CardDeploySpawn(card,sender);
            return true;
        }

        public void CardDeploySpawn(UnitCard card, Player sender)
        {
            
        }

        public void Spawn(UnitCard card, Player sender)
        {
            throw new NotImplementedException();
        }

        public void Spawn(Unit unit, Player sender)
        {

        }

        public void Kill(Unit unit)
        {
            throw new NotImplementedException();
        }

        public Unit GetUnit(Card card)
        {

        }
    }
}
