﻿using System;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Units;

namespace GameData.Controllers.Table
{
    public interface IActionTableControlller
    {
        TableCondition GetTableCondition { get; }
        void DrawCard(Player player);
        void DrawCard(Player player, int count);
        void DrawCard(Player player, Card card);
        void KillUnit(Unit unit);
        Card GetCard(int id);
        void SpawnUnit(Player sender, UnitCard card);
        void Remove(Unit unit);
    }

    /// <summary>
    ///     Класс контроллер, отвечающий за взаимодействие с полем внутри Action
    /// </summary>
    public class InActionTableController : IActionTableControlller
    {
        private readonly ICardDrawController _cardDrawController;
        private readonly IDataRepositoryController<Card> _cardRepositoryController;
        private readonly Lazy<IUnitDispatcher> _unitDispatcher;

        public InActionTableController(TableCondition tableCondition, ICardDrawController cardDrawController,
            Lazy<IUnitDispatcher> unitDispatcher, IDataRepositoryController<Card> cardRepositoryController)
        {
            GetTableCondition = tableCondition;
            _cardDrawController = cardDrawController;
            _unitDispatcher = unitDispatcher;
            _cardRepositoryController = cardRepositoryController;
        }

        public TableCondition GetTableCondition { get; }

        public void DrawCard(Player player)
        {
            _cardDrawController.DealCardsToPlayer(player, 1);
        }

        public void DrawCard(Player player, int count)
        {
            _cardDrawController.DealCardsToPlayer(player, count);
        }

        public void DrawCard(Player player, Card card)
        {
            _cardDrawController.DealCard(player, card);
        }

        public Card GetCard(int id)
        {
            return _cardRepositoryController.GetById(id);
        }

        public void KillUnit(Unit unit)
        {
            _unitDispatcher.Value.Kill(unit);
        }

        public void Remove(Unit unit)
        {
            _unitDispatcher.Value.Remove(unit);
        }

        public void SpawnUnit(Player sender, UnitCard card)
        {
            _unitDispatcher.Value.Spawn(card, sender);
        }
    }
}