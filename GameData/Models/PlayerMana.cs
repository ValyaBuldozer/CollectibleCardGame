using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models
{
    public class PlayerMana
    {
        public int Base { set; get; }

        public int Current { set; get; }

        public void Restore()
        {
            Current = Base;
        }
    }
}
