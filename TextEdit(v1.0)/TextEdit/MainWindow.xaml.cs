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

namespace TextEdit
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        bool Modified {  get; set; }
        public MainWindow()
        {
            InitializeComponent();
            textBox1.Focus();
            saveFileDialog.Filter = "Text files (*.txt) | *.txt";
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
    }
}
