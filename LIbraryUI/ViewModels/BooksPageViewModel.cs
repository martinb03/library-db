using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Threading.Tasks;
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
       var list = await _context.ViewBooks
            .ToListAsync();
       
        foreach (var book in list)
            Books.Add(book);
    }
}