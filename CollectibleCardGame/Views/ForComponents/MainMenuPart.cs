using System.Windows.Controls;

namespace CollectibleCardGame.Views.ForComponents
{
    public class MainMenuPart
    {
        public string Title { get; set; } // название

        public string ImagePath { get; set; } // путь к изображению

        public Page FramePage { set; get; }
    }
}