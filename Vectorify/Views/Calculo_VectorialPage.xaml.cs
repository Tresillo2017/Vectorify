using Microsoft.UI.Xaml.Controls;

using Vectorify.ViewModels;

namespace Vectorify.Views;

public sealed partial class Calculo_VectorialPage : Page
{
    public Calculo_VectorialViewModel ViewModel
    {
        get;
    }

    public Calculo_VectorialPage()
    {
        ViewModel = App.GetService<Calculo_VectorialViewModel>();
        InitializeComponent();
    }
}
