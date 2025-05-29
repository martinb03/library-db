using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LIbraryUI.Data.Models;
using LIbraryUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LIbraryUI.Views;

public partial class CustomersPageView : UserControl
{
    private ViewCustomer? _selectedCustomer;
    public CustomersPageView()
    {
        InitializeComponent();
        
        if (!Design.IsDesignMode)
            //return;

        
        {DataContext = App.ServiceProvider
            .GetRequiredService<CustomersPageViewModel>();}
    }
    
    private void CustomersGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // Grab the newly selected Customer (or null if none)
        _selectedCustomer = CustomersGrid.SelectedItem as ViewCustomer;

        // Show or hide the buttons
        var hasSelection = _selectedCustomer != null;
        BorrowingsButton.IsVisible = hasSelection;
        EditButton.IsVisible   = hasSelection;
        DeleteButton.IsVisible = hasSelection;
    }
    
    
    private void OnSelectCustomerClick(object? sender, RoutedEventArgs e)
    {
        var cust = (ViewCustomer)((Button)sender!).DataContext;
        CustomersGrid.SelectedItem = cust;
        ((CustomersPageViewModel)DataContext).SelectedCustomer = cust;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var cust = (ViewCustomer)((Button)sender!).DataContext;
        CustomersGrid.SelectedItem = cust;
        ((CustomersPageViewModel)DataContext).SelectedCustomer = cust;
    }

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_selectedCustomer == null) return;
    }
    private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_selectedCustomer == null) return;
    }

    private void BorrowingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_selectedCustomer == null) return;

        var borrowingsView = App.ServiceProvider
            .GetRequiredService<BorrowingsPageView>();

        if (borrowingsView.DataContext is BorrowingsPageViewModel vm)
        {
            vm.CustomerName = _selectedCustomer.Name;
            vm.SelectedCustomerId = _selectedCustomer.CustomerId ?? 0;
        }

        //MainWindow.FindControl<ContentControl>("ContentArea").Content = borrowingsView;
    }
}