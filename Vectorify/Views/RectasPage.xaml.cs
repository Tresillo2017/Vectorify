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
using Windows.UI;
using System.Reflection.Emit;


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

    private void DrawRuler(Canvas canvas)
    {
        var spacing = 50; // spacing between tick marks in pixels
        var tickLength = 10; // length of tick marks in pixels
        var fontSize = 12; // font size of coordinate labels

        // Draw vertical ruler
        for (int x = 0; x <= canvas.ActualWidth; x += spacing)
        {
            var tick = new Line
            {
                X1 = x,
                Y1 = 0,
                X2 = x,
                Y2 = tickLength,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };

            var label = new TextBlock
            {
                Text = x.ToString(),
                FontSize = fontSize,
                Margin = new Thickness(x, tickLength + 2, 0, 0)
            };

            canvas.Children.Add(tick);
            canvas.Children.Add(label);
        }

        // Draw horizontal ruler
        for (int y = 0; y <= canvas.ActualHeight; y += spacing)
        {
            var tick = new Line
            {
                X1 = 0,
                Y1 = y,
                X2 = tickLength,
                Y2 = y,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1
            };

            var label = new TextBlock
            {
                Text = y.ToString(),
                FontSize = fontSize,
                Margin = new Thickness(tickLength + 2, y - fontSize / 2, 0, 0)
            };

            canvas.Children.Add(tick);
            canvas.Children.Add(label);
        }
    }

    private void DrawVector(Canvas canvas, double endX, double endY)
    {
        // Calculate the center point of the canvas
        double centerX = canvas.ActualWidth / 2;
        double centerY = canvas.ActualHeight / 2;

        // Create a new Line element
        var line = new Line();

        // Set the start and end points of the line
        line.X1 = centerX;
        line.Y1 = centerY;
        line.X2 = endX;
        line.Y2 = endY;

        // Set the stroke color and thickness of the line
        line.Stroke = new SolidColorBrush(Colors.Red);
        line.StrokeThickness = 2;

        // Add the line to the canvas
        canvas.Children.Add(line);

        // Calculate the direction and length of the arrow
        double dirX = endX - centerX;
        double dirY = endY - centerY;
        double length = Math.Sqrt(dirX * dirX + dirY * dirY);

        // Calculate the normalized direction of the arrow
        double normDirX = dirX / length;
        double normDirY = dirY / length;

        // Calculate the perpendicular direction of the arrow
        double perpDirX = -normDirY;
        double perpDirY = normDirX;

        // Define the size of the arrow
        double arrowSize = 10;

        // Create a new Path element for the arrow
        var arrow = new Path();

        // Define the arrow shape
        var geometry = new PathGeometry();
        var figure = new PathFigure();
        figure.StartPoint = new Point(endX, endY);
        figure.Segments.Add(new LineSegment() { Point = new Point(endX - arrowSize * normDirX + arrowSize * perpDirX, endY - arrowSize * normDirY + arrowSize * perpDirY) });
        figure.Segments.Add(new LineSegment() { Point = new Point(endX - arrowSize * normDirX - arrowSize * perpDirX, endY - arrowSize * normDirY - arrowSize * perpDirY) });
        geometry.Figures.Add(figure);

        // Set the geometry of the arrow
        arrow.Data = geometry;

        // Set the stroke color and thickness of the arrow
        arrow.Stroke = new SolidColorBrush(Colors.Red);
        arrow.StrokeThickness = 2;

        // Add the arrow to the canvas
        canvas.Children.Add(arrow);
    }

    private void punto(double x, double y)
    {
        // Crear el punto
        Ellipse punto = new Ellipse();
        punto.Width = 5;
        punto.Height = 5;
        SolidColorBrush brush = new SolidColorBrush(Colors.Black);
        punto.Fill = brush;

        // Calcular el centro del canvas
        double centerX = canvas.ActualWidth / 2;
        double centerY = canvas.ActualHeight / 2;

        // Posicionar el punto en el canvas
        Canvas.SetLeft(punto, centerX + x - punto.Width / 2);
        Canvas.SetTop(punto, centerY - y - punto.Height / 2);

        // Agregar el punto al canvas
        canvas.Children.Add(punto);
    }


    private void AddGuideLines(Canvas canvas)
    {
        // Crea una línea horizontal y una vertical que pasen por el centro del canvas
        var guideLines = new[] {
            new { X1 = 0.0, Y1 = canvas.ActualHeight / 2, X2 = canvas.ActualWidth, Y2 = canvas.ActualHeight / 2 },
            new { X1 = canvas.ActualWidth / 2, Y1 = 0.0, X2 = canvas.ActualWidth / 2, Y2 = canvas.ActualHeight }
        };

        // Agrega cada línea al canvas
        foreach (var line in guideLines)
        {
            var shape = new Line
            {
                X1 = line.X1,
                Y1 = line.Y1,
                X2 = line.X2,
                Y2 = line.Y2,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 4.0, 2.0 } // línea punteada
            };

            Canvas.SetZIndex(shape, 100);
            canvas.Children.Add(shape);
        }


    }

    private void drawButton_Click(object sender, RoutedEventArgs e)
    {

        // Clear Canvas
        canvas.Children.Clear();

        //Draw Guide
        AddGuideLines(canvas);

        //Draw the ruler
        DrawRuler(canvas);

        // Get user input from text boxes
        double startX = 0;
        double.TryParse(ValorX_Vector.Text, out startX);
        double startY = 0;
        double.TryParse(ValorY_Vector.Text, out startY);

        // Get user input from text boxes
        double puntoX = 0;
        double.TryParse(ValorX_Punto.Text, out puntoX);
        double puntoY = 0;
        double.TryParse(ValorY_Punto.Text, out puntoY);

        double valorX = ValorX_Vector.Value;
        double valorY = ValorY_Vector.Value;

        // Get the end point from the user input
        double endX = double.Parse(ValorX_Vector.Text);
        double endY = double.Parse(ValorY_Vector.Text);

        // Call the DrawVector method with the canvas and end point
        DrawVector(canvas, endX, endY);

        // Draw point on canvas
        Ellipse point = new Ellipse();
        point.Width = 5;
        point.Height = 5;
        point.Fill = new SolidColorBrush(Colors.Green);
        Canvas.SetLeft(point, puntoX);
        Canvas.SetTop(point, puntoY);
        canvas.Children.Add(point);
    }
}
