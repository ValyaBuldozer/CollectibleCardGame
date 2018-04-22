using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.ActionParameters
{
    public class DamageActionParameter : IActionParameter
    {
        public int Value { set; get; }

        public void ChangeValue(int value)
        {
            Value += value;
        }
    }
}
