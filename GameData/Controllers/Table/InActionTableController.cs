using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models;
using GameData.Models.Units;

namespace GameData.Controllers.Table
{
    public interface IActionTableControlller
    {
        TableCondition GetTableCondition { get; }
        void DrawCard(Player player);
        void DrawCard(Player player, int count);
        void KillUnit(Unit unit);
    }

    /// <summary>
    /// Класс контроллер, отвечающий за взаимодействие с полем внутри Action
    /// </summary>
    public class InActionTableController : IActionTableControlller
    {
        private readonly ICardDrawController _cardDrawController;
        private readonly TableCondition _tableCondition;
        private readonly IUnitDispatcher _unitDispatcher;

        public TableCondition GetTableCondition => _tableCondition;

        public InActionTableController(TableCondition tableCondition, IUnitDispatcher unitDispatcher,
            ICardDrawController cardDrawController)
        {
            _tableCondition = tableCondition;
            _unitDispatcher = unitDispatcher;
            _cardDrawController = cardDrawController;
        }

        public void DrawCard(Player player)
        {
            _cardDrawController.DealCardsToPlayer(player,1);
        }

        public void DrawCard(Player player, int count)
        {
            _cardDrawController.DealCardsToPlayer(player,count);
        }

        public void KillUnit(Unit unit)
        {
            _unitDispatcher.Kill(unit);
        }


    }
}
