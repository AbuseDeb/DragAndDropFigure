using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOTP_LR_2
{
    public partial class MainWindow : Window
    {
        List<Point> m_plPoints = new List<Point>();
        FigureDirector m_pFigureDirector = new FigureDirector();

        public MainWindow()
        {
            InitializeComponent(); 
        }
        
        public void CreateTrangle(object sender, RoutedEventArgs e)
        {
            PatternCanvas.Children.Clear();
            m_plPoints.Clear();

            m_plPoints.Add(new Point(Convert.ToDouble(TrangleFirstDotX.Text), -Convert.ToDouble(TrangleFirstDotY.Text)));
            m_plPoints.Add(new Point(Convert.ToDouble(TrangleSecondDotX.Text), -Convert.ToDouble(TrangleSecondDotY.Text)));
            m_plPoints.Add(new Point(Convert.ToDouble(TrangleThirdDotX.Text), -Convert.ToDouble(TrangleThirdDotY.Text)));    
            
            TrangleBuilder pTrangle = new TrangleBuilder();

            Shape pFigure = m_pFigureDirector.MakeFigure(pTrangle, m_plPoints, GetColor());

            Canvas.SetLeft(pFigure, PatternCanvas.ActualWidth / 2);
            Canvas.SetTop(pFigure, PatternCanvas.ActualHeight / 2);

            PatternCanvas.Children.Add(pFigure);
        }

        public void CreateRectangle(object sender, RoutedEventArgs e)
        {
            PatternCanvas.Children.Clear();
            m_plPoints.Clear();

            m_plPoints.Add(new Point(Convert.ToDouble(RecFirstDotX.Text), -Convert.ToDouble(RecFirstDotY.Text)));
            m_plPoints.Add(new Point(Convert.ToDouble(RecSecondDotX.Text), -Convert.ToDouble(RecSecondDotY.Text)));

            RectangleBuilder pRectangle = new RectangleBuilder();
            Shape pFigure = m_pFigureDirector.MakeFigure(pRectangle, m_plPoints, GetColor());            

            Canvas.SetLeft(pFigure, PatternCanvas.ActualWidth / 2 - pFigure.Width / 2);
            Canvas.SetTop(pFigure, PatternCanvas.ActualHeight / 2 - pFigure.Height / 2);

            PatternCanvas.Children.Add(pFigure);
        }

        public void CreateCircle(object sender, RoutedEventArgs e)
        {
            PatternCanvas.Children.Clear();
            m_plPoints.Clear();

            m_plPoints.Add(new Point(Convert.ToDouble(CircleFirstDotX.Text), -Convert.ToDouble(CircleFirstDotY.Text)));

            CircleBuilder pCircle = new CircleBuilder();
            Shape pFigure = m_pFigureDirector.MakeFigure(pCircle, m_plPoints, GetColor());

            Canvas.SetLeft(pFigure, PatternCanvas.ActualWidth / 2 - pFigure.Width / 2);
            Canvas.SetTop(pFigure, PatternCanvas.ActualHeight / 2 - pFigure.Height / 2);

            PatternCanvas.Children.Add(pFigure);
        }

        private void PatternCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(PatternCanvas.Children.Count != 0)
            {
                Shape drag = (Shape)PatternCanvas.Children[0];
                DataObject data = new DataObject(typeof(Shape), PatternCanvas.Children[0]);
                DragDrop.DoDragDrop(PatternCanvas, data, DragDropEffects.Copy);
            }            
        }

        private void MainCanvas_Drop(object sender, DragEventArgs e)
        {
            UIElement source = null;
            source = (UIElement)sender;
            PatternCanvas.Children.Clear();
            Shape pShape = (Shape)e.Data.GetData(typeof(Shape));
            Mouse.Capture(source);
            if (pShape.GetType() == typeof(Polygon))
            {
                Canvas.SetLeft(pShape, Mouse.GetPosition(MainCanvas).X);
                Canvas.SetTop(pShape, Mouse.GetPosition(MainCanvas).Y);
            }
            else
            {
                Canvas.SetLeft(pShape, Mouse.GetPosition(MainCanvas).X - pShape.Width / 2);
                Canvas.SetTop(pShape, Mouse.GetPosition(MainCanvas).Y - pShape.Height / 2);
            }

            MainCanvas.Children.Add(pShape);
            Mouse.Capture(null);
        }

        SolidColorBrush GetColor()
        {
            switch (ColorBox.SelectedIndex)
            {
                case 0:
                    return Brushes.Red;
                case 1:
                    return Brushes.Green;
                case 2:
                    return Brushes.Blue;
                case 3:
                    return Brushes.Black;
            }
            return Brushes.Black;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9.-]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
