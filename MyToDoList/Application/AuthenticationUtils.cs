using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoList.Application;

class AuthenticationUtils
{
    public static async Task<bool> DoLogin(string username, string password)
    {
        var dto = await ApiClient.DoLogin(username, EncodeInMd5(password));
        if (DateTimeOffset.TryParse(dto?.expires_in, out DateTimeOffset date))
        {
            if (date.UtcDateTime > DateTimeOffset.UtcNow)
            {
                SessionContext.Instance.SetAccessToken(dto.access_token);
                SessionContext.Instance.SetTokenExpireDate(date);

                // DPAPI 
            }
        }
        return dto is not null;
    }

    public static async Task<bool> SignUp(string name, string surname, string username, string password, string email)
    {
        return await ApiClient.DoSignup(username, EncodeInMd5(password), name, surname, email);
    }

    private static string EncodeInMd5(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] encodedBytes = MD5.HashData(inputBytes);

        StringBuilder builder = new();
        for (int i = 0; i < encodedBytes.Length; i++)
        {
            builder.Append(encodedBytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}
