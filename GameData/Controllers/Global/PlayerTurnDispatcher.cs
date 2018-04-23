﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Repository;

namespace GameData.Controllers.Global
{
    public interface IPlayerTurnDispatcher
    {
        Player CurrentPlayer { get; }
        void NextPlayer();
        void Start(double interval);
        void Stop();
    }

    public class PlayerTurnDispatcher : IPlayerTurnDispatcher
    {
        private readonly TableCondition _tableCondition;
        private readonly CyclicQueue<Player> _playersCyclicQueue;
        private readonly ICardDrawController _cardsDispatcher;

        public Timer Timer { set; get; }

        public Player CurrentPlayer { private set; get; }

        public event EventHandler<PlayerTurnStartEventArgs> TurnStart; 

        public PlayerTurnDispatcher(TableCondition tableCondition,ICardDrawController cardsDispatcher)
        {
            _tableCondition = tableCondition;
            _cardsDispatcher = cardsDispatcher;

            _playersCyclicQueue = new CyclicQueue<Player>(tableCondition.Players);
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;
            CurrentPlayer = _playersCyclicQueue.Dequeue();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NextPlayer();
        }

        public void NextPlayer()
        {
            CurrentPlayer = _playersCyclicQueue.Dequeue();
            CurrentPlayer.Mana.Base++;
            CurrentPlayer.Mana.Restore();

            _cardsDispatcher.DealCardsToPlayer(CurrentPlayer,1);

            TurnStart?.Invoke(this,new PlayerTurnStartEventArgs(CurrentPlayer));

            if (Timer.Enabled)
            {
                Timer.Stop();
                Timer.Start();
            }
        }

        public void Start(double interval)
        {
            Timer.Enabled = false;
            Timer.Interval = interval;
            Timer.Enabled = true;
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
