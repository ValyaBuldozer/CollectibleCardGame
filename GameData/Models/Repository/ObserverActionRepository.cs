using System.Collections.ObjectModel;
using GameData.Models.Observer;

namespace GameData.Models.Repository
{
    public class ObserverActionRepository
    {
        public ObserverActionRepository()
        {
            Collection = new ObservableCollection<ObserverAction>();
        }

        public ObservableCollection<ObserverAction> Collection { get; }
    }
}