using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectibleCardGame.Models
{
    public class CurrentUser
    {
        //todo : переделать
        public string Username { set; get; }
        public int GameLoseCount { set; get; }
        public int GameWinCount { set; get; }
        public string NorthDeck { set; get; }
        public string SouthDeck { set; get; }
        public string DarkDeck { set; get; }
    }
}
