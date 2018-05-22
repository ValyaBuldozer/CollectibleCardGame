using System;
using System.Timers;
using GameData.Controllers.Table;
using GameData.Models;
using GameData.Models.Observer;
using GameData.Models.Repository;

namespace GameData.Controllers.Global
{
    public interface IPlayerTurnDispatcher
    {
        /// <summary>
        ///     Текущий игрок
        /// </summary>
        Player CurrentPlayer { get; }

        /// <summary>
        ///     Передать ход следущему игроку
        /// </summary>
        void NextPlayer();

        /// <summary>
        ///     Начать передачу ходов по таймеру
        /// </summary>
        void Start();

        /// <summary>
        ///     Остановать передачу ходов по таймеру
        /// </summary>
        void Stop();

        event EventHandler<TurnStartObserverAction> TurnStart;
    }

    public class PlayerTurnDispatcher : IPlayerTurnDispatcher
    {
        private readonly ICardDrawController _cardsDispatcher;
        private readonly GameSettings _settings;
        private readonly TableCondition _tableCondition;
        private CyclicQueue<Player> _playersCyclicQueue;

        public PlayerTurnDispatcher(TableCondition tableCondition, ICardDrawController cardsDispatcher,
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

        public Timer Timer { set; get; }

        public Player CurrentPlayer { private set; get; }

        public event EventHandler<TurnStartObserverAction> TurnStart;

        public void NextPlayer()
        {
            CurrentPlayer = _playersCyclicQueue.Dequeue();

            if (CurrentPlayer.State.Base < _settings.MaxPlayerMana)
                CurrentPlayer.State.Base++;

            CurrentPlayer.State.Restore();
            CurrentPlayer.TableUnits.ForEach(u => u.State.CanAttack = true);

            _cardsDispatcher.DealCardsToPlayer(CurrentPlayer, 1);

            TurnStart?.Invoke(this, new TurnStartObserverAction(CurrentPlayer.Username));

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
            if (_playersCyclicQueue.Count() == 0)
                _playersCyclicQueue = new CyclicQueue<Player>(_tableCondition.Players);

            Timer.Enabled = _settings.IsPlayerTurnTimerEnabled;
            NextPlayer();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NextPlayer();
        }
    }
}