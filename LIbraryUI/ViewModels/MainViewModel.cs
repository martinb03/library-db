using System.Reactive;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LIbraryUI.Data;
using LIbraryUI.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

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
    
    //public CustomersPageViewModel CustomersVM { get; }
    //public ReactiveCommand<Unit, Unit> GoToCustomersBorrowings { get; }
    
    
    public MainViewModel(CustomersPageViewModel customersVM)
    {
        //CustomersVM = customersVM;
        CurrentPage = App.ServiceProvider.GetRequiredService<CustomersPageViewModel>();;
        /*GoToCustomersBorrowings = ReactiveCommand.Create(() =>
        {
            var cust = CustomersVM.SelectedCustomer;
            if (cust == null)
                return;

            // 2) resolve a fresh BorrowingsPageViewModel
            var borrowVm = App.ServiceProvider
                .GetRequiredService<BorrowingsPageViewModel>();

            // 3) pass the parameters
            borrowVm.CustomerName       = cust.Name;
            borrowVm.SelectedCustomerId = cust.CustomerId!.Value;

            // 4) swap it in
            CurrentPage = borrowVm;
        });*/
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