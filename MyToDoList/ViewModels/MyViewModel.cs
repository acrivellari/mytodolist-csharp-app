using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoList.ViewModels;

public abstract class MyViewModel: ObservableObject
{
    public abstract void ResetVM();
}


public enum PageNames
{
    Login,
    Signup
};
