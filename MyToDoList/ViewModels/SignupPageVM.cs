using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDoList.ViewModels;

class SignupPageVM : MyViewModel
{
    private string? _name;
    private string? _surname;
    private string? _username;
    private string? _email;
    private string? _password;
    private IMessenger _messenger;

    public SignupPageVM(IMessenger messenger)
    {
        _messenger = messenger;
        Login = new RelayCommand(() => _messenger.Send<NavigationRequestMessage>(new(PageNames.Login)));
        Signup = new RelayCommand(async () => await SignUp());
    }

    public RelayCommand Signup { get; }
    public RelayCommand Login { get; }
    public string? Name { get { return _name; } set { SetProperty(ref _name, value); } }
    public string? Surname { get { return _surname; } set { SetProperty(ref _surname, value); } }
    public string? Username { get { return _username; } set { SetProperty(ref _username, value); } }
    public string? Email { get { return _email; } set { SetProperty(ref _email, value); } }
    public string? Password { get { return _password; } set { SetProperty(ref _password, value); } }

    public async Task SignUp()
    {
        if (Name is null || Surname is null || Username is null || Password is null || Email is null)
        {
            MessageBox.Show("Some fields are missing. Please compile the signup form.");
            return;
        }
        bool success = await AuthenticationUtils.SignUp(Name, Surname, Username, Password, Email);
        if (!success)
        {
            MessageBox.Show("Wrong credentials. Please try again...");
        }
        else
        {
            MessageBox.Show($"Success: created a new account with username - {Username}");
        }
    }

    public override void ResetVM()
    {
        Name = null;
        Surname = null;
        Username = null;
        Password = null;
        Email = null;
    }
}
