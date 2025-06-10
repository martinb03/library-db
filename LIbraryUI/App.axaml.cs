using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LIbraryUI.Data;
using LIbraryUI.Factories;
using LIbraryUI.ViewModels;
using LIbraryUI.Views;
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
        
        serviceCollection.AddTransient<MainView>();
        serviceCollection.AddTransient<BooksPageView>();
        serviceCollection.AddTransient<CustomersPageView>();
        serviceCollection.AddTransient<BorrowingsPageView>();
        serviceCollection.AddTransient<StaffPageView>();
        
        serviceCollection.AddSingleton<Func<Type, PageViewModel>>(x => type => type switch
        {
            _ when type == typeof(CustomersPageViewModel) => x.GetRequiredService<CustomersPageViewModel>(),
            _ when type == typeof(BorrowingsPageViewModel) => x.GetRequiredService<BorrowingsPageViewModel>(),
            _ when type == typeof(BooksPageViewModel) => x.GetRequiredService<BooksPageViewModel>(),
            _ when type == typeof(StaffPageViewModel) => x.GetRequiredService<StaffPageViewModel>(),
            _ => throw new InvalidOperationException(),
        });
        serviceCollection.AddSingleton<PageFactory>();
        
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