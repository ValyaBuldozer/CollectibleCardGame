using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Models;
using GameData.Models.Cards;
using GameData.Models.Observer;
using GameData.Models.Units;

namespace GameData.Controllers.Table
{
    public interface IUnitDispatcher
    { 
        /// <summary>
        /// Спавн юнита при розыгрше карты (с боевым кличем)
        /// </summary>
        /// <param name="card">Карта</param>
        /// <param name="sender">Игрок, разыгравший карту</param>
        /// <param name="actionTarget">Цель боевого клича</param>
        /// <returns></returns>
        bool CardPlayedSpawn(UnitCard card, Player sender, Unit actionTarget);

        void Kill(Unit unit);
        Unit GetUnit(UnitCard card);

        /// <summary>
        /// Спавн юнита без боевого клича
        /// </summary>
        /// <param name="card">Карта юнита</param>
        /// <param name="sender">Игрок</param>
        /// <returns></returns>
        bool Spawn(UnitCard card, Player sender);

        /// <summary>
        /// Выполняет атаку юнита
        /// </summary>
        /// <param name="sender">Атакующий юнит</param>
        /// <param name="target">Цель атаки</param>
        void HandleAttack(Unit sender, Unit target);

        /// <summary>
        /// Событие спавна юнита
        /// </summary>
        event EventHandler<UnitSpawnObserverAction> OnUnitSpawn;

        /// <summary>
        /// Событие смерти юнита
        /// </summary>
        event EventHandler<UnitDeathObserverAction> OnUnitDeath;

        /// <summary>
        /// Событие изменения состояия юнита - урон, хил, бафф
        /// </summary>
        event EventHandler<UnitStateChangeObserverAction> OnUnitStateChange;
    }

    public class UnitDispatcher : IUnitDispatcher
    {
        private readonly IGameActionController _actionController;
        private readonly IDataRepositoryController<Entity> _entityRepositoryController;
        private readonly GameSettings _settings;

        public UnitDispatcher(IGameActionController actionController, 
            IDataRepositoryController<Entity> entityRepositoryController,
            GameSettings settings)
        {
            _actionController = actionController;
            _entityRepositoryController = entityRepositoryController;
            _settings = settings;
        }

        public event EventHandler<UnitSpawnObserverAction> OnUnitSpawn;
        public event EventHandler<UnitDeathObserverAction> OnUnitDeath;
        public event EventHandler<UnitStateChangeObserverAction> OnUnitStateChange;

        /// <summary>
        /// Спавн юнита при розыгрше карты (с боевым кличем)
        /// </summary>
        /// <param name="card">Карта</param>
        /// <param name="sender">Игрок, разыгравший карту</param>
        /// <param name="actionTarget">Цель боевого клича</param>
        /// <returns></returns>
        public bool CardPlayedSpawn(UnitCard card, Player sender, Unit actionTarget)
        {
            if (sender.TableUnits.Count >= _settings.PlayerTableUnitsMaxCount)
                return false;

            var unit = GetUnit(card);

            if (unit == null)
                return false;

            unit.HealthPoint.ZeroHpEvent += OnUnitDies;
            unit.HealthPoint.DamageRecieved += OnUnitDamaged;
            unit.Player = sender;
            _actionController.ExecuteAction(unit.BattleCryActionInfo,sender,actionTarget);
            sender.TableUnits.Add(unit);
            _entityRepositoryController.AddNewItem(unit);

            OnUnitSpawn?.Invoke(this,new UnitSpawnObserverAction(unit));
            return true;
        }

        /// <summary>
        /// Спавн юнита без боевого клича
        /// </summary>
        /// <param name="card">Карта юнита</param>
        /// <param name="sender">Игрок</param>
        /// <returns></returns>
        public bool Spawn(UnitCard card, Player sender)
        {
            if (sender.TableUnits.Count >= 10)
                return false;

            var unit = GetUnit(card);

            if (unit == null)
                return false;

            unit.HealthPoint.ZeroHpEvent += OnUnitDies;
            unit.HealthPoint.DamageRecieved += OnUnitDamaged;
            unit.Player = sender;
            sender.TableUnits.Add(unit);
            _entityRepositoryController.AddNewItem(unit);

            OnUnitSpawn?.Invoke(this,new UnitSpawnObserverAction(unit));
            return true;
        }

        /// <summary>
        /// Уничтожает юнита
        /// </summary>
        /// <param name="unit">Юнит</param>
        public void Kill(Unit unit)
        {
            if (!unit.Player.TableUnits.Contains(unit)) return;
            unit.Player.TableUnits.Remove(unit);
            OnUnitDeath?.Invoke(this,new UnitDeathObserverAction(unit));
        }
        
        /// <summary>
        /// Выполняет атаку юнита
        /// </summary>
        /// <param name="sender">Атакующий юнит</param>
        /// <param name="target">Цель атаки</param>
        public void HandleAttack(Unit sender, Unit target)
        {
            if(target.AttackPriority == 0)
                //атака маскировки
                return;
            if(target.AttackPriority !=2 && target.Player.TableUnits.Exists(u=>u.AttackPriority == 2))
                //есть провокатор у противника
                return;

            _actionController.ExecuteAction(sender.OnAttackActionInfo,sender,target);
            target.HealthPoint.RecieveDamage(sender.Attack);
            sender.HealthPoint.RecieveDamage(target.Attack);
        }

        public Unit GetUnit(UnitCard card)
        {
            if(card == null)
                throw new NullReferenceException();

            return new Unit(card)
            {
                BattleCryActionInfo =
                    _actionController.GetGameActionInfo(card.BattleCryActionInfo),
                DeathRattleActionInfo =
                    _actionController.GetGameActionInfo(card.DeathRattleActionInfo),
                OnAttackActionInfo = 
                    _actionController.GetGameActionInfo(card.AttackActionInfo),
                OnDamageRecievedActionInfo = 
                    _actionController.GetGameActionInfo(card.DamageRecievedActionInfo)
            };
        }

        private void OnUnitDamaged(object sender, UnitRecievedDamageEventArgs e)
        {
            if(e.Unit?.OnDamageRecievedActionInfo == null) return;

            OnUnitStateChange?.Invoke(this,new UnitStateChangeObserverAction(e.Unit,e.Unit.EntityId));
            _actionController.ExecuteAction(e.Unit.OnDamageRecievedActionInfo,
                e.Unit, null);
        }

        private void OnUnitDies(object sender, ZeroHpEventArgs e)
        {
            if (e.Unit?.DeathRattleActionInfo != null)
                _actionController.ExecuteAction(e.Unit.DeathRattleActionInfo,
                    e.Unit.Player, null);

            Kill(e.Unit);
        }
    }
}
