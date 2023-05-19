using Microsoft.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Vectorify.ViewModels;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Data;

using System.Linq.Expressions;
namespace Vectorify.Views;

public sealed partial class FuncionesPage : Page
{
    public FuncionesViewModel ViewModel
    {
        get;
    }

    public FuncionesPage()
    {
        ViewModel = App.GetService<FuncionesViewModel>();
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //Clear the info bar
        infoBar.IsOpen = false;

        // Clear the canvas
        canvas2.Children.Clear();

        // Get the user input equation
        string equation = Input.Text;

        // Define the range of x-values
        double startX = -5;
        double endX = 5;
        double step = 0.1;

        // Generate points for the function plot
        List<Point> points = new List<Point>();
        for (double x = startX; x <= endX; x += step)
        {
            try
            {
                double y = EvaluateExpression(equation, x);
                points.Add(new Point(x, y));
            }
            catch (Exception ex)
            {
                // Show the InfoBar
                infoBar.IsOpen = true;
                return;
            }

        }

        // Draw function points
        foreach (Point point in points)
        {
            double canvasX = (point.X - startX) * (canvas2.ActualWidth / (endX - startX));
            double canvasY = canvas2.ActualHeight - (point.Y * (canvas2.ActualHeight / (endX - startX)));

            // Draw a point on the canvas
            Ellipse ellipse = new Ellipse
            {
                Width = 4,
                Height = 4,
                Fill = new SolidColorBrush(Colors.Red)
            };
            Canvas.SetLeft(ellipse, canvasX - 2);
            Canvas.SetTop(ellipse, canvasY - 2);
            canvas2.Children.Add(ellipse);
        }

        // Draw function line
        Polyline polyline = new Polyline
        {
            Stroke = new SolidColorBrush(Colors.Blue),
            StrokeThickness = 1
        };

        PointCollection pointCollection = new PointCollection();
        foreach (Point point in points)
        {
            double canvasX = (point.X - startX) * (canvas2.ActualWidth / (endX - startX));
            double canvasY = canvas2.ActualHeight - (point.Y * (canvas2.ActualHeight / (endX - startX)));

            pointCollection.Add(new Point(canvasX, canvasY));
        }

        polyline.Points = pointCollection;
        canvas2.Children.Add(polyline);
    }

    private double EvaluateExpression(string expression, double x)
    {
        DataTable dataTable = new DataTable();
        object result = dataTable.Compute(expression.Replace("x", x.ToString()), null);

        // Convert the result to double
        double convertedResult = Convert.ToDouble(result);

        return convertedResult;
    }
}


