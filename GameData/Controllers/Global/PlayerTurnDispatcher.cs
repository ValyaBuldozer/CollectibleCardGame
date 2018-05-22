using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GameData.Controllers.Data;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Observer;
using GameData.Models.Repository;

namespace GameData.Controllers.Global
{
    public interface IPlayerTurnDispatcher
    {
        /// <summary>
        /// Текущий игрок
        /// </summary>
        Player CurrentPlayer { get; }

        /// <summary>
        /// Передать ход следущему игроку
        /// </summary>
        void NextPlayer();

        /// <summary>
        /// Начать передачу ходов по таймеру
        /// </summary>
        void Start();

        /// <summary>
        /// Остановать передачу ходов по таймеру
        /// </summary>
        void Stop();
        event EventHandler<TurnStartObserverAction> TurnStart;
    }

    public class PlayerTurnDispatcher : IPlayerTurnDispatcher
    {
        private readonly TableCondition _tableCondition;
        private CyclicQueue<Player> _playersCyclicQueue;
        private readonly ICardDrawController _cardsDispatcher;
        private readonly GameSettings _settings;

        public Timer Timer { set; get; }

        public Player CurrentPlayer { private set; get; }

        public event EventHandler<TurnStartObserverAction> TurnStart; 

        public PlayerTurnDispatcher(TableCondition tableCondition,ICardDrawController cardsDispatcher,
            GameSettings settings)
        {
            _tableCondition = tableCondition;
            _cardsDispatcher = cardsDispatcher;
            _settings = settings;

            _playersCyclicQueue = new CyclicQueue<Player>(tableCondition.Players);
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;

            Timer.Interval = _settings.PlayerTurnInterval;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NextPlayer();
        }

        public void NextPlayer()
        {
            CurrentPlayer = _playersCyclicQueue.Dequeue();

            if(CurrentPlayer.State.Base < _settings.MaxPlayerMana)
                CurrentPlayer.State.Base++;

            CurrentPlayer.State.Restore();
            CurrentPlayer.TableUnits.ForEach(u=>u.State.CanAttack = true);

            _cardsDispatcher.DealCardsToPlayer(CurrentPlayer,1);

            TurnStart?.Invoke(this,new TurnStartObserverAction(CurrentPlayer.Username));

            if (Timer.Enabled)
            {
                //выключаем таймер чтобы не вызывать событие
                Timer.Enabled = false;
                Timer.Stop();
                Timer.Start();
                Timer.Enabled = true;
            }
        }

        public void Start()
        {
            if(_playersCyclicQueue.Count() == 0)
                _playersCyclicQueue = new CyclicQueue<Player>(_tableCondition.Players);

            Timer.Enabled = _settings.IsPlayerTurnTimerEnabled;
            NextPlayer();
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
