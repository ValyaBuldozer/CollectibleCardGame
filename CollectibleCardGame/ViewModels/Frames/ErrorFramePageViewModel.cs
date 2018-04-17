using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectibleCardGame.Services;

namespace CollectibleCardGame.ViewModels.Frames
{
    public class ErrorFramePageViewModel : BaseViewModel
    {
        private RelayCommand _reconnectCommand;

        public RelayCommand ReconnectCommand => _reconnectCommand ??
                                                (_reconnectCommand = new RelayCommand(obj =>
                                                {
                                                    //todo : доделать переподключение
                                                    throw new NotImplementedException();
                                                }));
    }
}
