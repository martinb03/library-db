using System;
using LIbraryUI.Data;
using LIbraryUI.ViewModels;

namespace LIbraryUI.Factories;

public class PageFactory(Func<Type, PageViewModel> factory)
{
    public PageViewModel GetPageViewModel<T>(Action<T> afterCreation = null)
        where T : PageViewModel
    {
        var viewModel = factory(typeof(T));
        afterCreation?.Invoke((T)viewModel);
        return viewModel;
    }
}