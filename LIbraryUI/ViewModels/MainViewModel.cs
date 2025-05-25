using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LIbraryUI.Data;
using LIbraryUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LIbraryUI.ViewModels;

public partial class MainViewModel : ViewModelsBase
{
    private const string buttonActiveClass = "active";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CustomersPageIsActive))]
    [NotifyPropertyChangedFor(nameof(BorrowingsPageIsActive))]
    [NotifyPropertyChangedFor(nameof(BooksPageIsActive))]
    [NotifyPropertyChangedFor(nameof(StaffPageIsActive))]
    private PageViewModel _currentPage;
    
    public bool CustomersPageIsActive => CurrentPage.PageName == ApplicationPageNames.Customers;
    public bool BorrowingsPageIsActive => CurrentPage.PageName == ApplicationPageNames.Borrowings;
    public bool BooksPageIsActive => CurrentPage.PageName == ApplicationPageNames.Books;
    public bool StaffPageIsActive => CurrentPage.PageName == ApplicationPageNames.Staff;
    
    
    public MainViewModel()
    {
        CurrentPage = App.ServiceProvider.GetRequiredService<CustomersPageViewModel>();;
    }

    [RelayCommand]
    private void GoToBooks()
    {
        CurrentPage = App.ServiceProvider.GetRequiredService<BooksPageViewModel>();
    }
    
    [RelayCommand]
    private void GoToBorrowings()
    {
        CurrentPage = App.ServiceProvider.GetRequiredService<BorrowingsPageViewModel>();
    }
    
    [RelayCommand]
    private void GoToCustomers()
    {
        CurrentPage = App.ServiceProvider.GetRequiredService<CustomersPageViewModel>();
    }
    
    
    [RelayCommand]
    private void GoToStaff()
    {
        CurrentPage = App.ServiceProvider.GetRequiredService<StaffPageViewModel>();
    }
}