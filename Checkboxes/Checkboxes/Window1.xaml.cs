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
using System.Windows.Shapes;

namespace Checkboxes
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (listBox1.SelectedItem as CheckBox).Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide(); 
            for(int i=0;i<3;i++)
            {
                ListBox lb = grid1.Children[i] as ListBox;
                var a = lb.Items.Cast<CheckBox>().Where(e1 => (bool)e1.IsChecked);
                var tb = (Owner as MainWindow).grid1.Children[i] as TextBox;
                var cb = (Owner as MainWindow).grid1.Children[i] as CheckBox;
                if (a.Count() == lb.Items.Count)
                {
                    tb.Text = "Вибрані всі пункти";
                    cb.IsChecked = true;
                }
                else if (a.Count() == 0)
                {
                    tb.Text = "Пункти не вибрані";
                    cb.IsChecked = false;
                }
                else
                {
                    tb.Text = a.Aggregate("Вибрано:", (acc,e1)=>acc+""+e1.Content);
                    cb.IsChecked = null;
                }

            }
        }

        private void listBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                var lb = e.Source as ListBox;
                if (lb == null)
                    return;
                var cb=lb.SelectedItems as CheckBox;
                cb.IsChecked = !(bool)cb.IsChecked;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (listBox1.SelectedItem as CheckBox).Focus();
        }
    }
}
