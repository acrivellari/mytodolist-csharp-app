using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyToDoList.Application;
using MyToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace MyToDoList.ViewModels;

public class LoginPageVM : MyViewModel
{
    private string? _username;
    private string? _password;
    private IMessenger _messenger;

    public LoginPageVM(IMessenger messenger)
    {
        _messenger = messenger;
        Login = new RelayCommand(async () => await DoLogin());
        Signup = new RelayCommand(() => _messenger.Send(new NavigationRequestMessage(PageNames.Signup)));
    }

    public string? Username { get => _username; set => SetProperty(ref _username, value); }
    public string? Password { get => _password; set => SetProperty(ref _password, value); }
    public RelayCommand Login { get; }
    public RelayCommand Signup { get; }

    public async Task DoLogin()
    {
        if (Username is null || Password is null)
        {
            MessageBox.Show("Some fields are missing. Please compile the login form.");
            return;
        }
        bool success = await AuthenticationUtils.DoLogin(Username, Password);
        if (!success) 
        {
            MessageBox.Show("Wrong credentials. Please try again...");
        }
        else
        {
            MessageBox.Show("Success");
            var sessionContext = SessionContext.Instance;
            sessionContext.SetLoggedInUser(Username);
            _messenger.Send(new NavigationRequestMessage(PageNames.Homepage));
        }
    }

    public override void ResetVM()
    {
        Username = null;
        Password = null;
    }
}


