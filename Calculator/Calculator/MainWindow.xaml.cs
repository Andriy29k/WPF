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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            textBox1.Focus();
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as Button ?? e.Source as Button;
            label1.Content = ((e.Source as Button).Content as string).TrimStart('_');
            label2.Content = "=";
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            double x = 0, x1, x2;
            if(!double.TryParse(textBox1.Text, out x1) || !double.TryParse(textBox2.Text, out x2))
            {
                label2.Content = "= ERROR";
                return;
            }
            switch(label1.Content as string)
            {
                case "+": x = x1 + x2; break;
                case "-": x = x1 - x2; break;
                case "*": x = x1 * x2; break;
                case "/": x = x1 / x2; break;
            }
            label2.Content = "=" + x;
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = e.Text[0];
            switch(c)
            {
                case '+': button1_Click(button1, null); break;
                case '_': button1_Click(button2, null); break;
                case '*': button1_Click(button3, null); break;
                case '/': button1_Click(button4, null); break;
            }
            e.Handled = !(char.IsDigit(c) || c =='-' || c == '\b' || 
                c == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);
        }

        private void StackPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (label2 != null)
            {
                label2.Content = '=';
            }
        }
    }
}
