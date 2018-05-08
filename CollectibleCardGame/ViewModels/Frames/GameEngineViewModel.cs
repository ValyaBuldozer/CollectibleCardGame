using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class GameEngineViewModel : BaseViewModel
    {
        private string _test;

        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                NotifyPropertyChanged(nameof(Test));
            }
        }

        public GameEngineViewModel()
        {
            Test = "test";
        }
    }
}
