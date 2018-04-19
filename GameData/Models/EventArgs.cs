using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models
{
    public class ZeroHpEventArgs : EventArgs
    {
        public Unit Unit { get; }

        public ZeroHpEventArgs(Unit unit)
        {
            Unit = unit;
        }
    }
}
