using System.Runtime.InteropServices.ObjectiveC;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Vectorify.ViewModels;
using Windows.Devices.Input;
using System.Windows.Input;
using Windows.Foundation;
using Path = Microsoft.UI.Xaml.Shapes.Path;

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

    private void DrawVector(Canvas canvas, double startX, double startY, double endX, double endY)
    {
        // Create a new Line element
        var line = new Line();

        // Set the start and end points of the line
        line.X1 = startX;
        line.Y1 = startY;
        line.X2 = endX;
        line.Y2 = endY;

        // Set the stroke color and thickness of the line
        line.Stroke = new SolidColorBrush(Colors.Red);
        line.StrokeThickness = 2;

        // Add the line to the canvas
        canvas.Children.Add(line);
    }

    
    

    private void drawButton_Click(object sender, RoutedEventArgs e)
    {
        // Get user input from text boxes
        double startX = 0;
        double.TryParse(ValorX_Vector.Text, out startX);
        double startY = 0;
        double.TryParse(ValorY_Vector.Text, out startY);

        // Clear Canvas
        canvas.Children.Clear();

        // Draw vector on canvas
        DrawVector(canvas, startX, startY, startX + 200, startY + 200);

    }



    /*void Calcular(object sender, RoutedEventArgs e)
    {
        Texto_Derecha.Text = "Calculando...";
    }*/

}
