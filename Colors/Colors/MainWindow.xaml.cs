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

namespace Colors
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Color, String> colorName = new Dictionary<Color, string>(141);
        public MainWindow()
        {
            InitializeComponent();

            AddHandler(Slider.ValueChangedEvent, 
                new RoutedPropertyChangedEventHandler<double>(slider1_ValueChanged));
            foreach (System.Reflection.PropertyInfo pi in typeof(Colors).GetProperties())
                colorName[(Color)pi.GetValue(null, null)] = pi.Name;
            slider1_ValueChanged(null, null);
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Title += "!";
            caption1.Background = new SolidColorBrush(Color.FromArgb((byte)slider1.Value,(byte)slider2.Value,
                (byte)slider3.Value,(byte)slider4.Value));
            
            var c = (caption1.Background as SolidColorBrush).Color;
            caption1.Foreground = new SolidColorBrush(Color.FromRgb((byte)(0xFF ^ c.R),
                (byte)(0xFF ^ c.G), (byte)(0xFF ^ c.B)));
            var s = c.ToString();
            switch(c.A)
            {
                case 0: s += "Transparent";
                    break;
                case 255:
                    if (colorName.ContainsKey(c)) s += "" + colorName[c];
                    break;
            }
            caption1.Content = s;
        }

        private void slider5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Title = "";
            RemoveHandler(Slider.ValueChangedEvent, 
                new RoutedPropertyChangedEventHandler<double>(slider1_ValueChanged));
            slider2.Value = slider3.Value = slider4.Value = slider5.Value;
            AddHandler(Slider.ValueChangedEvent, 
                new RoutedPropertyChangedEventHandler<double>(slider1_ValueChanged));
        }
    }
}
