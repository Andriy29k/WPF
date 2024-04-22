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


namespace IndividualTask1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window1 win1 =  new Window1();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Window1();
            dialog.Owner = this; 
            if (dialog.ShowDialog() == true)
            {
                double x, y;
                if (double.TryParse(dialog.txtX.Text, out x) && double.TryParse(dialog.txtY.Text, out y))
                {
                    var label = new System.Windows.Controls.Label();
                    label.Content = dialog.txtText.Text;
                    Canvas.SetLeft(label, x);
                    Canvas.SetTop(label, y);
                    canvas1.Children.Add(label);
                }
            }
        }
    }
}
