using Microsoft.UI.Xaml.Controls;

using Vectorify.ViewModels;

namespace Vectorify.Views;

public sealed partial class RectasPage : Page
{
    public RectasViewModel ViewModel
    {
        get;
    }

    public RectasPage()
    {
        ViewModel = App.GetService<RectasViewModel>();
        InitializeComponent();
    }
}
