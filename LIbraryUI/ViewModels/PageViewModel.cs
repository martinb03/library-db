using CommunityToolkit.Mvvm.ComponentModel;
using LIbraryUI.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LIbraryUI.ViewModels;

public partial class PageViewModel : ViewModelsBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;
}