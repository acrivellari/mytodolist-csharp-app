using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyToDoList.Application;
using MyToDoList.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDoList.ViewModels;

class SignupPageVM : MyViewModel, INotifyPropertyChanged
{
    private string? _name;
    private string? _surname;
    private string? _username;
    private string? _email;
    private string? _password;
    private string? _errorMessage = "";
    private bool _isEnabledSignup = false;
    private bool _isVisibleErrorMessage = true;
    private IMessenger _messenger;

    public SignupPageVM(IMessenger messenger)
    {
        _messenger = messenger;
        Login = new RelayCommand(() => _messenger.Send<NavigationRequestMessage>(new(PageNames.Login)));
        Signup = new RelayCommand(async () => await SignUp());

        PropertyChanged += HandlePropertyChanged;
    }

    private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        string[] fields = [nameof(Name), nameof(Surname), nameof(Username), nameof(Password), nameof(Email)];
        if (fields.Contains(e.PropertyName))
        {
            IsEnabledSignup = false;
            IsVisibleError = true;

            if (!(Name is not null && Name.Length > 0))
            {
                ErrorMessage = "Invalid name: too short.";
                return;
            }
            if (!(Surname is not null && Surname.Length > 0))
            {
                ErrorMessage = "Invalid surname: too short.";
                return;
            }
            if (!(Username is not null && Username.Length > 0))
            {
                ErrorMessage = "Invalid username: too short.";
                return;
            }
            if (!(Password is not null && Password.Length > 5))
            {
                ErrorMessage = "Invalid password: too short.";
                return;
            }
            if (!(Email is not null && Email.Length > 5))
            {
                ErrorMessage = "Invalid email: too short.";
                return;
            }
            if (!Regex.IsMatch(Email!, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                ErrorMessage = "Invalid email: use a valid email.";
                return;
            }

            IsEnabledSignup = true;
            IsVisibleError = false;
        }
    }

    public bool IsVisibleError { get { return _isVisibleErrorMessage; } set => SetProperty(ref _isVisibleErrorMessage, value); }
    public bool IsEnabledSignup { get { return _isEnabledSignup; } set => SetProperty(ref _isEnabledSignup, value); }
    public RelayCommand Signup { get; }
    public RelayCommand Login { get; }
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
    public string? Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                SetProperty(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
    }
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
