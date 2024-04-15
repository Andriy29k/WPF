using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Events
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            Canvas.SetLeft(button2, p.X - button2.ActualWidth/2);
            Canvas.SetTop(button2, p.Y - button2.ActualHeight/2);
            if ((string)button2.Content == "Змінити") 
            {
                button2.Content = "";
                button2.MouseMove += button2_MouseMove;
                button2.Click += button2_Click;
                button2.Click -= button2_Click2;
            }
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.Space)) return;
            Canvas.SetLeft(button2, r.NextDouble() * ((Content as Canvas).ActualWidth - 5));
            Canvas.SetTop(button2, r.NextDouble() * ((Content as Canvas).ActualHeight - 5));

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button2.Content = "Змінити";
            button2.MouseMove -= button2_MouseMove;
            button2.Click -= button2_Click;
            button2.Click += button2_Click2;
            
        }
        private void button2_Click2(object sender, RoutedEventArgs e) 
        { 
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        var c = Content as Canvas;
            for (int i = 0; i < 2; i++) 
            {
                var b = FindName("button" + (i + 1)) as Button;
                if(Canvas.GetLeft(b)>c.ActualWidth || Canvas.GetTop(b)>c.ActualHeight)
                {
                    Canvas.SetLeft(b, 10 + i * (b.ActualWidth + 10));
                    Canvas.SetTop(b, 10);
                }
            }
        }
    }
}
