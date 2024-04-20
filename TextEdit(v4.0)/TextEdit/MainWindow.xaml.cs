using System;
using System.IO;
using Microsoft.Win32;
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
using System.Windows.Forms;
using System.Windows.Controls.Primitives;

namespace TextEdit
{
    public partial class MainWindow : Window
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        MenuItem alignItem = new MenuItem();
        bool Modified {  get; set; }
        public MainWindow()
        {
            InitializeComponent();
            textBox1.Focus();
            saveFileDialog.Filter = "Text files (*.txt) | *.txt";
            alignItem = leftJustify1;
            leftJustify1.IsChecked = true;
            leftJustify1.Tag = HorizontalAlignment.Left;
            center1.Tag = HorizontalAlignment.Center;
            rightJustify1.Tag = HorizontalAlignment.Right;
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            leftJustify2.Tag = leftJustify1;
            center2.Tag = center1;
            rightJustify2.Tag = rightJustify1;
        }

        Color WinFormsColorToWPFColor(System.Drawing.Color c)
        {
            return Color.FromArgb(c.A,c.R,c.G,c.B);
        }

        System.Drawing.Color WPFColorToWinFormsColor(Color c)
        {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        void SaveToFile(string path)
        {
            File.WriteAllText(path, textBox1.Text, Encoding.Default);
            Modified = true;
        }

        bool TextSaved()
        {
            switch(MessageBox.Show("Зберегти зміни у файлі?", "Підтвердження", 
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    save0_Executed(null, null);
                    return !Modified;
                case MessageBoxResult.Cancel:
                    return false;
            }
            return true;
        }

        private void exit0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void save0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string path = saveFileDialog.FileName;
            if (path == "")
                saveAs0_Executed(null, null);
            else
                SaveToFile(path);
        }
        private void new0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(TextSaved())
            {
                textBox1.Clear();
                Title = "Текстовий редактор (v 1.0)";
                saveFileDialog.FileName = "";
                Modified = false;
            }
            
        }

        private void open0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(TextSaved())
            {
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == true)
                {
                    string path = openFileDialog.FileName;
                    textBox1.Text = File.ReadAllText(path, Encoding.Default);
                    Title = "Текстовий редактор (v 1.0) -" + path;
                    saveFileDialog.FileName = path;
                    Modified = false;
                }
            }
        }

        private void saveAs0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var oldPath = saveFileDialog.FileName;
            saveFileDialog.FileName = System.IO.Path.GetFileName(saveFileDialog.FileName);
            if(saveFileDialog.ShowDialog() ==true)
            {
                string path = saveFileDialog.FileName;
                SaveToFile(path);
                Title="Text editor (v 1.0) - " + path;
            }
            else
            {
                saveFileDialog.FileName = oldPath;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !TextSaved();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Modified = true;
        }

        private void save0_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {   
            e.CanExecute = Modified;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                switch(e.Key)
                {
                    case Key.B:
                        bold1_Click(null, null);
                        e.Handled = true;
                        break;
                    case Key.I:
                        italic1_Click(null,null); 
                        e.Handled = true;
                        break;
                    case Key.U:
                        underline1_Click(null, null);
                        e.Handled = true;
                        break;
                    case Key.L:
                        leftJustify1_Click(leftJustify1, null);
                        e.Handled = true;
                        break;
                    case Key.E:
                        leftJustify1_Click(center1,null);
                        e.Handled = true;
                        break;
                    case Key.R:
                        leftJustify1_Click(rightJustify1, null);
                        e.Handled = true;
                        break;

                }
        }

        private void bold1_Click(object sender, RoutedEventArgs e)
        {
            bold2.IsChecked = bold1.IsChecked = !bold1.IsChecked;
            textBox1.FontWeight = bold1.IsChecked ? FontWeights.Bold : FontWeights.Normal;
        }

        private void italic1_Click(object sender, RoutedEventArgs e)
        {
            italic2.IsChecked = italic1.IsChecked = !italic1.IsChecked;
            textBox1.FontStyle = italic1.IsChecked ? FontStyles.Italic : FontStyles.Normal;
        }

        private void underline1_Click(object sender, RoutedEventArgs e)
        {
            underline2.IsChecked = underline1.IsChecked = !underline1.IsChecked;
            textBox1.TextDecorations = underline1.IsChecked ? TextDecorations.Underline : null;
        }

        private void leftJustify1_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem ??
                (sender as ToogleButton).Tag as MenuItem;
            alignItem.IsChecked = false;
            alignItem = mi;
            mi.IsChecked = true;
            textBox1.HorizontalAlignment = (HorizontalAlignment)mi.Tag;
        }

        private void fontColor0_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void backColor0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            colorDialog.Color = WPFColorToWinFormsColor((textBox1.Foreground as SolidColorBrush).Color);
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBox1.Foreground =
                    new SolidColorBrush(WinFormsColorToWPFColor(colorDialog.Color));
        }

        private void undo0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox1.Undo();
        }

        private void undo0_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = textBox1.CanUndo;
        }

        private void cut0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox1.Cut();
        }

        private void cut0_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = textBox1 != null && textBox1.SelectedText.Length > 0;
        }

        private void copy0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox1.Copy();
        }

        private void copy0_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void paste0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox1.Paste();
        }

        private void paste0_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = System.Windows.Clipboard.ContainsText();
        }

        private void selectAll0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void selectAll0_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= textBox1.Text.Length>0;
        }

        private void save2_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var b = sender as Button;
            b.Opacity = b.IsEnabled ? 1 : 0.5;
        }
    }
}
