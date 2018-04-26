using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CollectibleCardGame.Views.ForComponents
{
    class MainMenuPart
    {
        public string Title { get; set; } // название

        public string ImagePath { get; set; } // путь к изображению

        public Page FramePage { set; get; }
    }
}
