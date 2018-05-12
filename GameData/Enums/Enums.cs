
namespace GameData.Enums
{
    public enum MessageBaseType
    {
        LogInMessage,
        RegistrationMessage,
        UserInfoRequestMessage,
        SetDeckMesage,
        GameRequestMessage,
        GameStartMessage,
        PlayerTurnMessage,
        PlayerTurnStartMessage,
        GameResultMessage,
        DisconnectMessage,
        ErrorMessage,
        ObserverActionMessage
    }

    public enum Fraction
    {
        North,
        South,
        Dark
    }

    public enum PlayerSequencing
    {
        FirstPlayer,
        SecondPlayer
    }

    public enum ActionType
    {
        BattleCry,
        DeathRattle,
        OnAttack,
        Spell,
        Other
    }

    public enum ActionParameterType
    {
        Damage,
        DamageRandomRange,
        Heal,
        HealRandomRange,
        Buff,
        BuffRandomRange,
        Empty
    }

    public enum SpellTargetType
    {
        AlliedUnits,
        EnemyUnits,
        AlliedUnit,
        EnemyUnit,
        Table
    }

    public enum GameEndReason
    {
        HeroUnitKill,
        PlayerSurrender,
        PlayerDisconnected
    }

    public enum PlayerTurnType
    {
        CardDeploy,
        UnitAttack,
        TurnEnd,

    }

    public enum ObserverActionType
    {
        GameStart,
        CardDeploy,
        CardDraw,
        UnitSpawn,
        UnitDeath,
        UnitStateChange,
        PlayerStateChange,
        GameAction,
        TurnStart,
        TurnEnd,
        Error
    }

    public enum EntityType
    {
        Unit,
        Card,
        Player,
        Scene
    }
}
