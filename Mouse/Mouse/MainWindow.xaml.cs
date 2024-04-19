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

namespace Mouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point p;
        int maxz;
        Size s;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rect1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (e.Source as UIElement).CaptureMouse();
            if (e.Source == canvas1) return;
            var a = e.Source as FrameworkElement;
            p = e.GetPosition(a);
            Canvas.SetZIndex(a, ++maxz);
            s = new Size(a.ActualWidth, a.ActualHeight);
            if (e.LeftButton == MouseButtonState.Pressed)
                a.Cursor = Cursors.Hand;
            else
                if (e.LeftButton == MouseButtonState.Pressed)
                a.Cursor = Cursors.SizeNWSE;
            e.Handled = true;
        }

        private void rect1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Source == canvas1)
            {
                Title = "Mouse"; return;
            }
            var a = e.Source as FrameworkElement;
            Point q = e.GetPosition(a);
            Title = string.Format("Mouse - {0} {1}", a.Name, q);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(a, Math.Max(0, Canvas.GetLeft(a) + q.X - p.X));
                Canvas.SetTop(a, Math.Max(0,Canvas.GetTop(a) + q.Y - p.Y));

            }
            else
                if(e.RightButton == MouseButtonState.Pressed)
                {
                    a.Width = Math.Max(20, s.Width+q.X-p.X);
                    a.Height = Math.Max(20, s.Height+q.Y-p.Y);
                }
            e.Handled= true;
        }

        private void rect1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var a = e.Source as FrameworkElement;
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(a != canvas1)
                    a.Cursor = Cursors.Hand;
            }
            else
            {
                if (e.RightButton == MouseButtonState.Pressed) 
                {
                    if (a != canvas1) 
                    {
                        a.Cursor = Cursors.SizeNWSE;
                    }
                }
                else
                {
                    (e.Source as FrameworkElement).ReleaseMouseCapture();
                    (e.Source as FrameworkElement).Cursor = null;
                }
            }
            
        }
    }
}
