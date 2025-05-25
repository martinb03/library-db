using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LIbraryUI.Data;
using LIbraryUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LIbraryUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static ServiceProvider ServiceProvider { get; private set; }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        
        var connectionString = Environment.GetEnvironmentVariable("LIBRARY_DB_CONN")
                               ?? throw new InvalidOperationException("Missing LIBRARY_DB_CONN env var");
        serviceCollection.AddDbContext<LibraryContext>(opts => opts.UseNpgsql(connectionString));
        
        
        serviceCollection.AddSingleton<MainViewModel>();
        
        serviceCollection.AddTransient<BooksPageViewModel>();
        serviceCollection.AddTransient<CustomersPageViewModel>();
        serviceCollection.AddTransient<BorrowingsPageViewModel>();
        serviceCollection.AddTransient<StaffPageViewModel>();
        
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        ServiceProvider = serviceProvider;
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainView
            {
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}