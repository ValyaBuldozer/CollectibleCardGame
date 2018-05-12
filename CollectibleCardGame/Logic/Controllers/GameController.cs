﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Network.Controllers;
using CollectibleCardGame.ViewModels.Frames;
using CollectibleCardGame.ViewModels.Windows;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network.Messages;
using Unity.Interception.Utilities;

namespace CollectibleCardGame.Logic.Controllers
{
    public class GameController
    {
        private readonly INetworkController _networkController;

        public GameController(INetworkController networkController,
            GoGameFramePageViewModel goGameFramePageViewModel,GameEngineViewModel gameEngineViewModel)
        {
            _networkController = networkController;
            goGameFramePageViewModel.GameRequest += GameRequestEventHandler;
            gameEngineViewModel.PlayerTurnEvent += ViewModelPlayerTurnEventHandler;
        }

        private void ViewModelPlayerTurnEventHandler(object sender, Services.PlayerTurnRequestEventArgs e)
        {
            if(e.PlayerTurn == null)
                return;

            MessageBase message = new MessageBase(MessageBaseType.PlayerTurnMessage,
                new PlayerTurnMessage(){PlayerTurn = e.PlayerTurn});
            _networkController.SendMessage(message);
        }

        private void GameRequestEventHandler(object sender, Services.GameRequestEventArgs e)
        {
            Random rnd = new Random();
            var array = new int[] {20,20,20,21,21,21,22,22,22,23,23,23,24,24,24,25,25,25,26,26,26,27,27,27};
            for (int i = 0; i < 16; i++)
            {
                array[rnd.Next(0, 15)] = array[i];
            }

            var deck = new List<int>(array);
            
            var card = new UnitCard()
            {
                BaseHP = 30,
                BaseAttack = 0,
                AttackPriority = 1,
                Name = "TestHero"
            };
            SendGameRequest(deck,card);
        }

        public void SendGameRequest(List<int> deck, UnitCard card)
        {
            if(deck == null || card == null)
                throw new NullReferenceException();

            MessageBase message = new MessageBase(MessageBaseType.GameRequestMessage,
                new GameRequestMessage()
                {
                    CardDeckIdList = deck,
                    HeroUnitCard = card
                });
            _networkController.SendMessage(message);
        }

        public void PrepareToGame()
        {

        }
    }
}
