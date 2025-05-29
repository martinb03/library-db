using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LIbraryUI.Data;
using LIbraryUI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LIbraryUI.ViewModels;

public partial class BorrowingsPageViewModel : PageViewModel
{
    private readonly LibraryContext _context;

    public ObservableCollection<ViewCustomerBorrowing> Borrowings { get; } = new();
    
    public BorrowingsPageViewModel(LibraryContext context)
    {
        
        PageName = ApplicationPageNames.Borrowings;
        CustomerName = "Not Selected";
        
        _context = context;
        _ = LoadBorrowingsAsync();
    }

    

    

    private async Task LoadBorrowingsAsync()
    {

        IQueryable<ViewCustomerBorrowing> query = _context.ViewCustomerBorrowings;
        if (SelectedCustomerId != 0)
        {
            query = query.Where(b => b.CustomerId == SelectedCustomerId);
        }

        if (ShowActiveOnly)
        {
            query = query.Where(b => b.ReturnDate == null);
        }

        if (ShowOverdueOnly)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            query = query.Where(b => 
                b.ReturnDate == null && 
                b.DueDate < today);
        }
            
        
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            if (SelectedSearchCriteria == "Book Title")
            {
                query = query.Where(b => b.BookTitle.Contains(SearchText));
            }
            else if (SelectedSearchCriteria == "Customer Name")
            {
                query = query.Where(b => b.Customer.Contains(SearchText));
            }
        }
        
        var list = await query.ToListAsync();
        
         Borrowings.Clear();
         foreach (var borrowing in list)
             Borrowings.Add(borrowing);
    }
    
    public List<string> SearchCriteriaOptions { get; } = new()
    {
        "Book Title",
        "Customer Name"
    };
    private string _selectedSearchCriteria = "Book Title";
    public string SelectedSearchCriteria
    {
        get => _selectedSearchCriteria;
        set
        {
            if (_selectedSearchCriteria == value) return;
            _selectedSearchCriteria = value;
            OnPropertyChanged(nameof(SelectedSearchCriteria));
            _ = LoadBorrowingsAsync();
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
            _ = LoadBorrowingsAsync();
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    
    private bool _showActiveOnly;
    public bool ShowActiveOnly
    {
        get => _showActiveOnly;
        set
        {
            if (_showActiveOnly == value) return;
            _showActiveOnly = value;
            OnPropertyChanged(nameof(ShowActiveOnly));
            _ = LoadBorrowingsAsync();
        }
    }
    
    private bool _showOverdueOnly;
    public bool ShowOverdueOnly
    {
        get => _showOverdueOnly;
        set
        {
            if (_showOverdueOnly == value) return;
            _showOverdueOnly = value;
            OnPropertyChanged(nameof(ShowOverdueOnly));
            _ = LoadBorrowingsAsync();
        }
    }

    
    private string _customerName;
    public string CustomerName
    {
        get => _customerName;
        set
        {
            if (_customerName == value) return;
            _customerName = value;
            OnPropertyChanged(nameof(CustomerName));
        }
    }
    
    private int _selectedCustomerId;
    public int SelectedCustomerId
    {
        get => _selectedCustomerId;
        set
        {
            if (_selectedCustomerId == value) return;
            _selectedCustomerId = value;
            OnPropertyChanged(nameof(SelectedCustomerId));
            
            _ = LoadBorrowingsAsync();
        }
    }
}