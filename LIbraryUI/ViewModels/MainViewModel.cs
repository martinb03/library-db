using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LIbraryUI.Views;

namespace LIbraryUI.ViewModels;

public partial class MainViewModel : ViewModelsBase
{
    private const string buttonActiveClass = "active";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BooksPageIsActive))]
    [NotifyPropertyChangedFor(nameof(BorrowingsPageIsActive))]
    [NotifyPropertyChangedFor(nameof(CustomersPageIsActive))]
    [NotifyPropertyChangedFor(nameof(PenaltiesPageIsActive))]
    [NotifyPropertyChangedFor(nameof(StaffPageIsActive))]
    private ViewModelsBase _currentPage;
    
    
    public bool BooksPageIsActive => CurrentPage == _booksPage;
    public bool BorrowingsPageIsActive => CurrentPage == _borrowingsPage;
    public bool CustomersPageIsActive => CurrentPage == _customersPage;
    public bool PenaltiesPageIsActive => CurrentPage == _penaltiesPage;
    public bool StaffPageIsActive => CurrentPage == _staffPage;
    
    private readonly BooksPageViewModel _booksPage = new ();
    private readonly BorrowingsPageViewModel _borrowingsPage = new ();
    private readonly CustomersPageViewModel _customersPage = new ();
    private readonly PenaltiesPageViewModel _penaltiesPage = new ();
    private readonly StaffPageViewModel _staffPage = new ();

    public MainViewModel()
    {
        CurrentPage = _booksPage;
    }

    [RelayCommand]
    private void GoToBooks()
    {
        CurrentPage = _booksPage;
    }
    
    [RelayCommand]
    private void GoToBorrowings()
    {
        CurrentPage = _borrowingsPage;
    }
    
    [RelayCommand]
    private void GoToCustomers()
    {
        CurrentPage = _customersPage;
    }
    
    [RelayCommand]
    private void GoToPenalties()
    {
        CurrentPage = _penaltiesPage;
    }
    
    [RelayCommand]
    private void GoToStaff()
    {
        CurrentPage = _staffPage;
    }
}