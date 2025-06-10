using System.Reactive;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LIbraryUI.Data;
using LIbraryUI.Factories;
using LIbraryUI.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace LIbraryUI.ViewModels;

public partial class MainViewModel : ViewModelsBase
{
    private PageFactory _pageFactory;
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
    
    //public CustomersPageViewModel CustomersVM { get; }
    //public ReactiveCommand<Unit, Unit> GoToCustomersBorrowings { get; }
    
    
    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        GoToCustomers();
    }

    [RelayCommand]
    private void GoToCustomers() => CurrentPage = _pageFactory.GetPageViewModel<CustomersPageViewModel>();

    [RelayCommand]
    private void GoToBorrowings() => CurrentPage = _pageFactory.GetPageViewModel<BorrowingsPageViewModel>
        (afterCreation => afterCreation.CustomerName="Selected Customer");
    
    [RelayCommand]
    private void GoToBooks() => CurrentPage = _pageFactory.GetPageViewModel<BooksPageViewModel>();
    
    [RelayCommand]
    private void GoToStaff() => CurrentPage = _pageFactory.GetPageViewModel<StaffPageViewModel>();
}