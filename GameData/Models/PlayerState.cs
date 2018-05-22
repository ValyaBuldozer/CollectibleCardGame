using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameData.Models
{
    public class PlayerState
    {
        private int _base;
        private int _current;
        private int _deckCardsCount;

        public int DeckCardsCount
        {
            get => _deckCardsCount;
            set
            {
                if(_deckCardsCount == value) return;

                _deckCardsCount = value;
                Changed?.Invoke(Player,new PlayerManaChangeEventArgs(this));
            }
        }

        public int Base
        {
            get => _base;
            set
            {
                if(_base == value) return;

                _base = value;
                Changed?.Invoke(Player,new PlayerManaChangeEventArgs(this));
            }
        }

        public int Current
        {
            get => _current;
            set
            {
                if(_current == value) return;

                _current = value;
                Changed?.Invoke(Player,new PlayerManaChangeEventArgs(this));
            }
        }

        [JsonIgnore]
        public Player Player { set; get; }

        public void Restore()
        {
            Current = Base;
        }

        public event EventHandler<PlayerManaChangeEventArgs> Changed;

        public override string ToString()
        {
            return Current + " / " + Base;
        }
    }
}
