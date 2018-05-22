using System;
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