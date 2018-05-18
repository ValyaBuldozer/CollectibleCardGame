using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        event EventHandler<EntityStateChangeObserverAction> OnUnitStateChange;

        void OnUnitStateChanges(object sender, PropertyChangedEventArgs e);
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
        public event EventHandler<EntityStateChangeObserverAction> OnUnitStateChange;

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

            unit.State.ZeroHpEvent += OnUnitDies;
            unit.State.PropertyChanged += OnUnitStateChanges;
            unit.Player = sender;
            _actionController.ExecuteAction(unit.BattleCryActionInfo,unit,actionTarget);
            sender.TableUnits.Add(unit);
            _entityRepositoryController.AddNewItem(unit);

            OnUnitSpawn?.Invoke(this,new UnitSpawnObserverAction(unit,unit.Player.Username));
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

            unit.State.ZeroHpEvent += OnUnitDies;
            unit.State.PropertyChanged += OnUnitStateChanges;
            unit.Player = sender;
            sender.TableUnits.Add(unit);
            _entityRepositoryController.AddNewItem(unit);

            OnUnitSpawn?.Invoke(this,new UnitSpawnObserverAction(unit,unit.Player.Username));
            return true;
        }

        /// <summary>
        /// Уничтожает юнита
        /// </summary>
        /// <param name="unit">Юнит</param>
        public void Kill(Unit unit)
        {
            if(unit == null) return;
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
            if(target.State.AttackPriority == 0)
                //атака маскировки
                return;

            if(target.State.AttackPriority !=2 && target.Player.TableUnits.Exists(u=>u.State.AttackPriority == 2))
                //есть провокатор у противника
                return;

            if(!sender.State.CanAttack)
                return;

            _actionController.ExecuteAction(sender.OnAttackActionInfo,sender,target);
            target.State.RecieveDamage(sender.State.Attack);
            sender.State.RecieveDamage(target.State.Attack);
            sender.State.CanAttack = false;
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

        public void OnUnitStateChanges(object sender, PropertyChangedEventArgs e)
        {
            if(!(sender is UnitState unitState))
                return;

            OnUnitStateChange?.Invoke(this,new EntityStateChangeObserverAction(
                unitState.Unit.EntityId,unitState.Unit));
        }

        private void OnUnitDamaged(object sender, UnitRecievedDamageEventArgs e)
        {
            if(e.Unit?.OnDamageRecievedActionInfo == null) return;

            //OnUnitStateChange?.Invoke(this,new UnitStateChangeObserverAction(e.Unit,e.Unit.EntityId));
            //_actionController.ExecuteAction(e.Unit.OnDamageRecievedActionInfo,
            //    e.Unit, null);
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
