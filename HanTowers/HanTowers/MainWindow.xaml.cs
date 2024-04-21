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

namespace HanTowers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        int total;
        int count;
        int minCount;
        public MainWindow()
        {
            InitializeComponent();
            comboBox1.ItemsSource = Enumerable.Range(2,9).Select(e=>e.ToString());
            comboBox1.Focus();
        }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new Action(delegate { }));
        }

        private void Step(int k, DockPanel src, DockPanel dst, DockPanel tmp)
        {
            if (k == 0) return;
            Step(k - 1, src, tmp, dst);
            if(button1.IsEnabled) return;
            MoveReact(src.Children[src.Children.Count - 1] as Rectangle, dst);
            DoEvents();
            System.Threading.Thread.Sleep(1500/(total -1));
            Step(k-1, tmp, dst, src);
        }
        void Info()
        {
            label1.Text = string.Format("Число переміщень: {0} ({1})", count, minCount);
        }

        DockPanel GetPanel(object trg)
        {
            var panel = trg as DockPanel;
            if(panel != null) return panel;
            var r = trg as Rectangle;
            if(r != null)
                return r.Parent as DockPanel;
            return null;
        }

        void MoveReact(Rectangle r, DockPanel panel)
        {
            (r.Parent as DockPanel).Children.Remove(r);
            panel.Children.Add(r);
            count++;
            Info();
            if(panel2.Children.Count == total || panel3.Children.Count == total)
                label2.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox1.SelectedIndex = 8;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            total = int.Parse(comboBox1.SelectedItem.ToString());
            panel1.Children.Clear();
            panel2.Children.Clear();
            panel3.Children.Clear();
            var w = (panel1.ActualWidth-20)/(2*total);
            count = 0;
            minCount = (int)Math.Round(Math.Pow(2, total)) - 1;
            Info();
            label2.Visibility = Visibility.Hidden;
            for (int i = 0; i < total; i++)
            {
                var r = new Rectangle();
                r.Width = panel1.ActualWidth - (20 + i * 2 * w);
                r.Height = (panel1.ActualHeight - 10) / total;
                r.StrokeThickness = 1;
                DockPanel.SetDock(r, Dock.Bottom);
                LinearGradientBrush b = new LinearGradientBrush();
                b.StartPoint = new Point(0.5, 0);
                b.EndPoint = new Point(0.5, 0.5);
                Color c1 = Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
                b.GradientStops.Add(new GradientStop(Colors.White, 0));
                b.GradientStops.Add(new GradientStop(c1, 1));
                r.Fill = b;
                r.RadiusX = r.Height / 8;
                r.RadiusY = r.Height / 8;
                panel1.Children.Add(r);
                r.MouseDown += r_MouseDown;
            }

        }

        void r_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            if(!button1.IsEnabled) return;
            if(e.ChangedButton==MouseButton.Left)
                DragDrop.DoDragDrop(sender as Rectangle, sender, DragDropEffects.Move);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {

            var panel = GetPanel(e.Source);
            if (panel == null) return;
            var r = e.Data.GetData(typeof(Rectangle)) as Rectangle;
            var oldPanel = r.Parent as DockPanel;
            e.Effects = oldPanel.Children.IndexOf(r) == oldPanel.Children.Count - 1 ?
                DragDropEffects.Move : DragDropEffects.None;
            e.Handled = true;
        }

        private void panel1_Drop(object sender, DragEventArgs e)
        {
            var panel = GetPanel(e.Source);
            if(panel == null) return;
            var r = e.Data.GetData(typeof (Rectangle)) as Rectangle;
            if(panel == r.Parent) return;
            MoveReact(r, panel);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            comboBox1_SelectionChanged(null,null);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            comboBox1.IsEnabled = button1.IsEnabled = !button1.IsEnabled;
            if(!button1.IsEnabled)
            {
                if(panel1.Children.Count != total)
                    comboBox1_SelectionChanged(null,null);
                Step(total, panel1, panel3, panel2);
                comboBox1.IsEnabled = button1.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            button1.IsEnabled = true;
        }
    }
}
