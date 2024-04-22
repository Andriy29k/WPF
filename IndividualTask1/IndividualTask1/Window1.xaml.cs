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

namespace IndividualTask1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private double previousX = 0;
        private double previousY = 0;
        private bool usePreviousCoordinates = false;
        public Window1()
        {
            InitializeComponent();
            this.KeyDown += AddLabelDialog_KeyDown;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }

        private void AddLabelDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Ok.IsDefault)
                    Apply_Click(sender, e);
                else if (Apply.IsDefault)
                    Apply_Click(sender, e);
            }
            else if (e.Key == Key.Escape)
            {
                if (Cancel.IsCancel)
                    Cancel_Click(sender, e);
            }
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (usePreviousCoordinates)
            {

                txtX.Text = (previousX + 20).ToString();
                txtY.Text = (previousY + 20).ToString();
            }
            else
            {

                txtX.Text = "";
                txtY.Text = "";
            }
            txtX.Focus();
        }

        private void SetPreviousCoordinates(double x, double y)
        {
            previousX = x;
            previousY = y;
            usePreviousCoordinates = true;
        }
    }
}
