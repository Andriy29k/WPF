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

namespace WorkWithWindows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window1 win1 = new Window1();
        Window2 win2 = new Window2();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            win1.Owner = this;
            win2.Owner = this;
            win1.Left = this.Left + this.ActualWidth - 10;
            win1.Top = this.Top + this.ActualHeight - 10;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (win1.IsVisible)
                win1.Close();
            else
                win1.Show();

        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            win2.ShowDialog();
            if (win2.DialogRes)
                win2.button22_Click(null, null);
        }
    }
}
