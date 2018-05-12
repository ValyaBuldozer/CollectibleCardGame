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

        public PlayerMana PlayerMana { set; get; }

        public PlayerStateChangesObserverAction(string username, PlayerMana mana)
        {
            PlayerMana = mana;
            PlayerUsername = username;
            Type = ObserverActionType.PlayerStateChange;
        }

        public PlayerStateChangesObserverAction()
        {
            Type = ObserverActionType.PlayerStateChange;
        }
    }
}
