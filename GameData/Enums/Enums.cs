
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
        ErrorMessage
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
}
