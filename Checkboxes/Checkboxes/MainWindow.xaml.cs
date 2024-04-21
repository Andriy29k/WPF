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

namespace Checkboxes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window1 win1 = new Window1();
        public MainWindow()
        {
            InitializeComponent();
            win1.listBox1.ItemsSource = MakeCheckBoxList(5);
            win1.listBox1.ItemsSource = MakeCheckBoxList(4);
            win1.listBox1.ItemsSource = MakeCheckBoxList(6);
            for(int i = 0; i < 3; i++)
                (win1.grid1.Children[i] as ListBox).SelectedIndex = 0;
            button1.Focus();
        }

        IEnumerable<CheckBox> MakeCheckBoxList(int count)
        {
            return Enumerable.Range(1, count).Select(e =>
            {
                var res = new CheckBox();
                res.Content = e.ToString();
                res.IsTabStop = false;
                return res;
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            win1.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            var cb = e.Source as CheckBox;
            if (cb != null) return;
            var i = Grid.GetRow(cb);
            var tb = grid1.Children[i] as TextBlock;
            tb.Text = cb.IsChecked == true ? "Вибрані всі пункти" : "Пункти не вибрані";
            var lb = win1.grid1.Children[i] as ListBox;
            foreach ( var e1 in lb.Items.Cast<CheckBox>())
                e1.IsChecked=cb.IsChecked;
        }
    }
}
