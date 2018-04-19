using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.ActionParameters
{
    public interface IActionParameter
    {
        void ChangeValue(int value);
    }
}
