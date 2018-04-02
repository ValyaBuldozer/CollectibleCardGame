
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
}
