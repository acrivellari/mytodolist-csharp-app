using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MyToDoList.Models;
using System.Windows;

namespace MyToDoList.ViewModels;

public class MainWindowViewModel : ObservableObject, IRecipient<NavigationRequestMessage>
{
    private ObservableObject? _currentVm;
    private Dictionary<PageNames, MyViewModel> _viewModels;
    private IMessenger messenger;

    public MainWindowViewModel()
    {
        messenger = WeakReferenceMessenger.Default;
        messenger.Register(this);

        _viewModels = new Dictionary<PageNames, MyViewModel>()
        {
            { PageNames.Login, new LoginPageVM(messenger) },
            { PageNames.Signup, new SignupPageVM(messenger) },
            { PageNames.Homepage, new HomePageVM(messenger) }
        };

        Receive(new NavigationRequestMessage(PageNames.Login));
    }

    public ObservableObject? CurrentViewModel
    {
        get => _currentVm;
        set => SetProperty(ref _currentVm, value);
    }

    public void Receive(NavigationRequestMessage message)
    {
        if (_viewModels.TryGetValue(message.Value, out var vm))
        {
            CurrentViewModel = vm;
            vm.ResetVM();
        }
        else
        {
            MessageBox.Show("Error in loading new page");
        }
    }
}
