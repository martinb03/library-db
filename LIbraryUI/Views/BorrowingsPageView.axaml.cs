using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LIbraryUI.Data.Models;

namespace LIbraryUI.Views;

public partial class BorrowingsPageView : UserControl
{
    public BorrowingsPageView()
    {
        InitializeComponent();

    }

     private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
     {
        if (e.PropertyName.EndsWith("Id"))
         {
             e.Cancel = true;
         }
         if (e.PropertyName == "IsOverdue")
         {
             e.Cancel = true;
         }

         
     }

     
     
}