using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Models.Observer;

namespace GameData.Models.Repository
{
    public class ObserverActionRepository
    {
        public ObservableCollection<ObserverAction> Collection { get; }

        public ObserverActionRepository()
        {
            Collection = new ObservableCollection<ObserverAction>();
        }
    }
}
