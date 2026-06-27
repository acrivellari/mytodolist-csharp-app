using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoList.Application;

/// <summary>
/// Static singleton, lazy loading, pattern.
/// </summary>
public sealed class SessionContext : INotifyPropertyChanged
{
    private class Nested
    {
        internal static readonly SessionContext instance = new SessionContext();
        static Nested() { }
    }

    public static SessionContext Instance { get => Nested.instance; }
    private SessionContext() { }

    public event PropertyChangedEventHandler PropertyChanged;
    public string? Username { get; private set; } = null;
    public string? AccessToken { get; private set; } = null;
    public DateTimeOffset? TokenExpiresIn { get; private set; } = null;

    public void SetLoggedInUser(string username)
    {
        if (username != Username) 
        { 
            Username = username;
            OnPropertyChanged(nameof(Username));
        }
    }

    public void Logout()
    {
        Username = null;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SetAccessToken(string accessToken)
    {
        AccessToken = accessToken;
    }

    public void SetTokenExpireDate(DateTimeOffset expiresIn)
    {
        TokenExpiresIn = expiresIn;
    }
}
