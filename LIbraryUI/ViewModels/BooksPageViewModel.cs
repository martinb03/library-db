using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LIbraryUI.Data;
using LIbraryUI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LIbraryUI.ViewModels;

public partial class BooksPageViewModel() : PageViewModel
{
    private readonly LibraryContext _context;

    public ObservableCollection<ViewBook> Books { get; } = new();
    
    public BooksPageViewModel(LibraryContext context) : this()
    {
        PageName = ApplicationPageNames.Books;
        _context = context;
        _ = LoadBooksAsync();
    }

    private async Task LoadBooksAsync()
    {
        IQueryable<ViewBook> query = _context.ViewBooks;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            if (SelectedSearchCriteria == "Title")
            {
                query = query.Where(b => b.Title.Contains(SearchText));
            }
            else if (SelectedSearchCriteria == "Author")
            {
                query = query.Where(b => b.Authors.Contains(SearchText));
            }
        }
        
        if (ShowOnlyAvailable)
        {
            query = query.Where(b => b.Status == "available");
        }

        if (SelectedCondition != "All")
        {
            query = query.Where(b => b.Condition == SelectedCondition);
        }
        
        var list = await query.ToListAsync();
       
       Books.Clear();
        foreach (var book in list)
            Books.Add(book);
    }

    public List<string> SearchCriteriaOptions { get; } = new()
    {
        "Title",
        "Author"
    };
    
    private string _selectedSearchCriteria = "Title";
    public string SelectedSearchCriteria
    {
        get => _selectedSearchCriteria;
        set
        {
            if (_selectedSearchCriteria == value) return;
            _selectedSearchCriteria = value;
            OnPropertyChanged(nameof(SelectedSearchCriteria));
            _ = LoadBooksAsync();
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
            _ = LoadBooksAsync();
        }
    }
    
    private bool _showOnlyAvailable;
    public bool ShowOnlyAvailable
    {
        get => _showOnlyAvailable;
        set
        {
            if ( _showOnlyAvailable == value )return;
            _showOnlyAvailable = value;
            OnPropertyChanged(nameof(ShowOnlyAvailable));
            _ = LoadBooksAsync();
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    public List<string> ConditionOptions { get; } = new()
    {
        "All",
        "new",
        "gently used",
        "worn",
        "damaged"
    };
    private string _selectedCondition = "All";

    public string SelectedCondition
    {
        get => _selectedCondition;
        set
        {
            if ( _selectedCondition == value )return;
            _selectedCondition = value;
            OnPropertyChanged(nameof(SelectedCondition));
            _ = LoadBooksAsync();
        }
    }
}