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

namespace TrigonFunc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    class TFData
    {
        public double X { get; set; }
        public double SinX { get; set; }
        public double CosX { get; set; }
        public double TgX { get; set; }
        public double CtgX { get; set; }

        public TFData(double arg)
        {
            X = arg;
            SinX = Math.Sin(X);
            CosX = Math.Cos(X);
            TgX = SinX / CosX;
            CtgX = 1 / TgX;
        }

    }

    
        public partial class MainWindow : Window
        {

        List<TFData> tf = new List<TFData>();
        Window1 win1 = new Window1();


        public MainWindow()
        {
            InitializeComponent();
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new Action(delegate { }));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutUpdated -= Window_LayoutUpdated;
            Mouse.OverrideCursor = Cursors.AppStarting;
            win1.Owner = this;
            win1.Show();
            int n = 7, nMax = 1000001;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                try
                {
                    int n0 = int.Parse(args[1]);
                    if (n0 < 2 || n0 > nMax)
                        throw new Exception();
                    else
                        n = n0;
                }
                catch
                {
                    string s = string.Format("Неправильний параметр: {0}\n" + "Допустиме значення: від 2 до {1}", args[1], nMax);
                    MessageBox.Show(s,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }
                double step = 1.0 / (n - 1);
                for (int i = 0; i < n; i++)
                {
                    if(win1.progressBar1.Value!=100*i/(n-1))
                    {
                        win1.progressBar1.Value = 100 * i / (n - 1);
                        DoEvents();
                    }
                    tf.Add(new TFData(Math.PI * i * step));
                }
                listView1.ItemsSource = tf;
            }
            System.Threading.Thread.Sleep(5000);
            win1.Hide();
            Mouse.OverrideCursor = null;
            LayoutUpdated += Window_LayoutUpdated;
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            double maxWidth = gridView1.Columns.Select(e1 => e1.ActualWidth).Max() + 10;
            for(int i = 1;i<gridView1.Columns.Count;i++)
                gridView1.Columns[i].Width = maxWidth;
            LayoutUpdated -= Window_LayoutUpdated;
            DoEvents();
            Left = (SystemParameters.WorkArea.Width - ActualWidth) / 2;
        }
    }
}
