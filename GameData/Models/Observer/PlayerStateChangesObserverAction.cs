using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Models.Observer
{
    public class PlayerStateChangesObserverAction : ObserverAction
    {
        public string PlayerUsername { set; get; }

        public PlayerState PlayerState { set; get; }

        public PlayerStateChangesObserverAction(string username, PlayerState state)
        {
            PlayerState = state;
            PlayerUsername = username;
            Type = ObserverActionType.PlayerStateChange;
        }

        public PlayerStateChangesObserverAction()
        {
            Type = ObserverActionType.PlayerStateChange;
        }
    }
}
