using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Enums;

namespace GameData.Models.Observer
{
    public class ErrorObserverAction : ObserverAction
    {
        public string ErrorMessage { set; get; }

        public bool IsSystemError { set; get; }

        public ErrorObserverAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Type = ObserverActionType.Error;
        }

        public ErrorObserverAction()
        {
            Type = ObserverActionType.Error;
        }
    }
}
