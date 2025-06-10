using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using LIbraryUI.Data;
using LIbraryUI.Data.Models;
using LIbraryUI.Factories;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace LIbraryUI.ViewModels;

public class CustomersPageViewModel : PageViewModel
{
    private readonly LibraryContext _context;
    public ObservableCollection<ViewCustomer> Customers { get; } = new();
    
    
    public CustomersPageViewModel(LibraryContext context)
    {
        PageName = ApplicationPageNames.Customers;
        _context = context;
        _ = LoadCustomersAsync();
        
        
        
    }

    private async Task LoadCustomersAsync()
    {
        IQueryable<ViewCustomer> query = _context.ViewCustomers;

        if (ShowActiveOnly)
        {
           
            query = query.Where(c => c.ActiveBorrowings != 0);
            
        }
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            if (SelectedSearchCriteria == "Name")
            {
                query = query.Where(c => c.Name.Contains(SearchText));
            }
            else if (SelectedSearchCriteria == "Contacts")
            {
                query = query.Where(c => c.ContactInfo.Contains(SearchText));
            }
            else if (SelectedSearchCriteria == "ID")
            {
                query = query.Where(c => c.CustomerId.ToString().Contains(SearchText));
            }
        }
        
        var list = await query.ToListAsync();
        Customers.Clear();
        foreach (var customer in list)
        {
            Customers.Add(customer);
        }
    }
    private bool _showActiveOnly;
    public bool ShowActiveOnly
    {
        get => _showActiveOnly;
        set
        {
            if (_showActiveOnly == value) return;
            _showActiveOnly = value;
            OnPropertyChanged(nameof(ShowActiveOnly));
            _ = LoadCustomersAsync();
        }
    }
    
    public List<string> SearchCriteriaOptions { get; } = new()
    {
        "Name",
        "Contacts",
        "ID"
    };
    private string _selectedSearchCriteria = "Name";
    public string SelectedSearchCriteria
    {
        get => _selectedSearchCriteria;
        set
        {
            if (_selectedSearchCriteria == value) return;
            _selectedSearchCriteria = value;
            OnPropertyChanged(nameof(SelectedSearchCriteria));
            _ = LoadCustomersAsync();
        }
    }
    
    private string _searchText = "";
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value) return;
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
            _ = LoadCustomersAsync();
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    private ViewCustomer? _selectedCustomer;
    

    public ViewCustomer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (_selectedCustomer == value) return;
            _selectedCustomer = value;
            OnPropertyChanged(nameof(SelectedCustomer));
            
            OnPropertyChanged(nameof(CanEditOrDelete));
        }
    }
    public bool CanEditOrDelete => SelectedCustomer != null;
    
    public int SelectedCustomerId => SelectedCustomer?.CustomerId ?? 0;

    
    
}