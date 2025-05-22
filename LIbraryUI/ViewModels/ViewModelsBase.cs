using CommunityToolkit.Mvvm.ComponentModel;

namespace LIbraryUI.ViewModels;

public class ViewModelsBase : ObservableObject
{
    public ViewModelsBase()
    {
        if (Avalonia.Controls.Design.IsDesignMode)
            OnDesignTimeConstructor();
    }
    protected virtual void OnDesignTimeConstructor() { }
}