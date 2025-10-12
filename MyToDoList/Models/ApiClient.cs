using Flurl;
using Flurl.Http;
using MyToDoList.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoList.Models;

class ApiClient
{
    private const string BaseUrl = "https://acriveeu.netsons.org/api";
    public static async Task<bool> DoLogin(string username, string pwd)
    {
        try
        {
            var json = await BaseUrl
                .AppendPathSegment("users")
                .AppendPathSegment("login")
                .PostJsonAsync(new DoLoginRequestBody 
                { 
                    username = username, 
                    password = pwd 
                })
                .ReceiveJson<DoLoginResponse>();
            return true;
        }
        catch { }
        return false;
    }

    public static async Task<bool> DoSignup(string username, string password, string name, string surname, string email)
    {
        try
        {
            var json = await BaseUrl
                .AppendPathSegment("users")
                .AppendPathSegment("signup")
                .PostJsonAsync(new DoSignupRequestBody 
                { 
                    email = email,
                    name = name,
                    surname = surname,
                    username = username, 
                    password = password 
                })
                .ReceiveJson<DoSignupResponse>();
            return true;
        }
        catch { }
        return false;
    }
}
