using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CollectibleCardGame.Services;
using CollectibleCardGame.Views.Frames;

namespace CollectibleCardGame.ViewModels.Frames
{
    class LogInFramePageShellViewModel: BaseViewModel
    {
    private Page _currentFramePage;
    private LogInFramePage _logInFramePage;
    private ToRegisterFramePage _toRegisterFramePage;
    private RelayCommand _switchFrameCommand;

    public Page CurrentFramePage
    {
        get => _currentFramePage;
        set
        {
            _currentFramePage = value;
            NotifyPropertyChanged(nameof(CurrentFramePage));
        }
    }

    public RelayCommand SwitchFrameCommand
    {
        get => _switchFrameCommand ?? (_switchFrameCommand = new RelayCommand(obj =>
        {
            if (_currentFramePage is LogInFramePage)
                _currentFramePage = _toRegisterFramePage;
            if (_currentFramePage is ToRegisterFramePage)
                _currentFramePage = _logInFramePage;
        }));
    }

    public LogInFramePageShellViewModel()
    {
        _logInFramePage = new LogInFramePage();
        _toRegisterFramePage = new ToRegisterFramePage();
        _currentFramePage = _logInFramePage;
        _logInFramePage.ToRegisterButton.Click += ToRegisterButton_Click;

    }

    private void ToRegisterButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        CurrentFramePage = _toRegisterFramePage;
    }
    }
}
