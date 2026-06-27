using CommunityToolkit.Mvvm.ComponentModel;

namespace MyToDoList.ViewModels;

public abstract class MyViewModel: ObservableObject
{
    public abstract void ResetVM();
}

public enum PageNames
{
    Login,
    Signup,
    Homepage
};
