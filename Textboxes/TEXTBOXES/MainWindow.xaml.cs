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

namespace TEXTBOXES
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush backgr;
        Brush foregr;
        Brush bordbr;
        Thickness bordth;
        public MainWindow()
        {   
            InitializeComponent();
            grid1.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(textBox1_TextChanged));
            backgr = textBox1.Background;
            foregr = textBox1.Foreground;
            bordbr = textBox1.BorderBrush;
            bordth = textBox1.BorderThickness;
            textBox1.Focus();
        }

        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            if (tb == null)
                return;
            tb.Foreground = Brushes.White;
            tb.Background = Brushes.Green;
            tb.Select(tb.Text.Length, 0);
        }

        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            if(tb == null) return;
            tb.Foreground = foregr;
            tb.Background = backgr;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var e1 in grid1.Children)
            {
                TextBox tb = e1 as TextBox;
                if (tb != null)
                    tb.TabIndex = int.MaxValue;
            }
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            for(int i=0;i <grid1.Children.Count-1; i++)
            {
                TextBox tb = grid1.Children[i] as TextBox;
                tb.TabIndex = i * (int)Math.Pow(10, i % 3);
            }
        }

        private void grid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    radioButton1.IsChecked = true; break;
                case Key.F3:
                    radioButton1.IsChecked= true; break;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            if(tb.Text =="")
            {
                tb.BorderBrush = Brushes.Red;
                tb.BorderThickness = new Thickness(1.01);
                tb.ToolTip = "Поле не може бути порожнім";
            }
            else
            {
                tb.BorderBrush= bordbr;
                tb.BorderThickness = bordth;
                tb.ToolTip = null;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool res = false;
            foreach (var e1 in grid1.Children)
                if((e1 as Control).BorderBrush == Brushes.Red)
                {
                    res = true;
                    break;
                }
            e.Cancel = res;
        }
    }
}
