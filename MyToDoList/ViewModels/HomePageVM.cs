using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyToDoList.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDoList.ViewModels;

public class HomePageVM : MyViewModel, INotifyPropertyChanged
{
    private IMessenger _messenger;
    private string _text = "";
    private readonly SessionContext _session = SessionContext.Instance;

    public HomePageVM(IMessenger messenger)
    {
        _messenger = messenger;
        LoginCommand = new RelayCommand(() => MessageBox.Show(_session.AccessToken));
        _session.PropertyChanged += Session_PropertyChanged;
    }

    private void Session_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        DisplayedUsername = _session.Username ?? "";
    }

    public RelayCommand LoginCommand { get; }

    public string DisplayedUsername
    {
        get { return _text; }
        set => SetProperty(ref _text, value);
    }

    public override void ResetVM() { }
}
