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
using System.IO;
using IOPath = System.IO.Path;
using Microsoft.Win32;

namespace ImageViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] imageExst = { ".bmp", ".jpeg", ".jpg", ".png", ".gif", ".wmf", "emf", ".ico" };
        string regKeyName = "SoftWare\\WPFExamples\\ImageView";
        public MainWindow()
        {
            InitializeComponent();
            TreeViewItem item = new TreeViewItem();
            dirList1.Tag = item;
            item.Tag = null;
            item.Header = "Комп'ютер";
            item.Items.Add("*");
            dirList1.Items.Add(item);
            item.IsSelected = true;
            dirList1.Focus();
            dirList1.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(TreeViewItem_Expanded));
            comboBox1.ItemsSource=Enum.GetValues(typeof(Stretch));
            comboBox1.SelectedIndex=0;
            string s = "";
            int i = 0;
            RegistryKey rk = null;
            try 
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if(rk !=null)
                {
                    Width = (int)rk.GetValue("Width", (int)Width);
                    Height = (int)rk.GetValue("Height", (int)Height);
                    grid1.ColumnDefinitions[0].Width = new GridLength((int)rk.GetValue("DirList",
                        (int)grid1.ColumnDefinitions[0].Width.Value));
                    grid1.ColumnDefinitions[2].Width = new GridLength((int)rk.GetValue("FileList",
                            (int)grid1.ColumnDefinitions[2].Width.Value));
                    comboBox1.SelectedIndex = (int)rk.GetValue("Stretch", comboBox1.SelectedIndex);
                    s = (string)rk.GetValue("Path", "");
                    i = (int)rk.GetValue("File", 0);
                }
            }
            finally
            {
                if( rk != null )
                    rk.Close();
            }
            if(!Directory.Exists(s))
                s = Directory.GetCurrentDirectory();
            item = InitialExpanding(s);
            if(item != null )
                item.IsSelected = true;
            if (fileList1.Items.Count == 0)
                i = -i;
            else
                if (i >= fileList1.Items.Count || i == -1)
                i = 0;
            fileList1.SelectedIndex = i;
            item = InitialExpanding(Directory.GetCurrentDirectory());
            if(item != null )
                item.IsSelected = true;
            dirList1.Focus();
            dirList1.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(TreeViewItem_Expanded));
        }

        void ExpandItem(TreeViewItem item)
        {
            item.Items.Clear();
            if (item.Tag == null)
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (!drive.IsReady)
                        continue;
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = drive.RootDirectory;
                    newItem.Header = drive.Name;
                    if (drive.VolumeLabel != "")
                    {
                        newItem.Header += "[" + drive.VolumeLabel + "]";
                    }
                    if (drive.RootDirectory.GetDirectories().Length > 0)
                    {
                        newItem.Items.Add("*");
                    }
                    item.Items.Add(newItem);
                }
            }
            else
            {
                try
                {
                    foreach (var subDir in (item.Tag as DirectoryInfo).GetDirectories())
                    {
                        try
                        {
                            TreeViewItem newItem = new TreeViewItem();
                            newItem.Tag = subDir;
                            newItem.Header = subDir.Name;
                            if (subDir.GetDirectories().Length > 0) newItem.Items.Add("*");
                            item.Items.Add(newItem);
                        }
                        catch { }
                    }
                }
                catch { }
            }
            item.IsExpanded = true;
        }

        TreeViewItem InitialExpanding(string fullPath)
        {
            if(!Directory.Exists(fullPath))
                return null;
            var paths = fullPath.Split('\\');
            paths[0] += "\\";
            TreeViewItem rootItem = dirList1.Items[0] as TreeViewItem;
            ExpandItem(rootItem);
            TreeViewItem item = rootItem;
            foreach (var e in paths) 
            { 
                item=item.Items.Cast<TreeViewItem>().FirstOrDefault(e1 =>(e1.Tag as DirectoryInfo).
                Name.ToUpper()==e.ToUpper());
                if(item==null) return null;
                ExpandItem(item);
            }
            return item;
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandItem(e.Source as TreeViewItem);
        }

        private void dirList1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Return)
            { 
                var tv = dirList1.SelectedItem as TreeViewItem;
                tv.IsExpanded = !tv.IsExpanded;
            }
        }

        private void dirList1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var dirInfo = (dirList1.SelectedItem as TreeViewItem).Tag as DirectoryInfo;
            if(dirInfo == null)
                fileList1.ItemsSource = null;
            else 
            {
                try
                {
                    var src = dirInfo.GetFiles().Select(e1 => e1.Name).Where(e1 => imageExst.
                        Contains(IOPath.GetExtension(e1).ToLower()));
                    fileList1.ItemsSource = src;
                    if(src.Count()>0)
                    {
                        fileList1.SelectedIndex = 0;
                    }
                }
                catch
                {
                    fileList1.ItemsSource=null;
                }
            }
        }

        private void fileList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = (string)fileList1.SelectedValue;
            if (name != null)
            {
                name = ((dirList1.SelectedItem as TreeViewItem).Tag as DirectoryInfo).FullName + "\\" + name;
                Title = "Image Viewer - " + name;
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    image1.Source = new BitmapImage(new Uri(name));
                    Title += "(" + (int)image1.Source.Width + "x" + (int)image1.Source.Height + ")";
                }
                catch
                {
                    Title += "(WRONG FORMAT)";
                    image1.Source = null;
                }
                Mouse.OverrideCursor = null;
            }
            else
            {
                Title = "Imgae Viewer";
                image1.Source= null;
            }
            scrollViewer1.Focusable = image1.Source!=null;
            scrollViewer1.IsTabStop = image1.Source != null;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            image1.Stretch = (Stretch)comboBox1.SelectedValue;
            if(image1.Stretch == Stretch.None)
            {
                scrollViewer1.HorizontalScrollBarVisibility =
                    scrollViewer1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                image1.Width = image1.Height = double.NaN;
            }
            else
            {
                scrollViewer1.HorizontalScrollBarVisibility = scrollViewer1.VerticalScrollBarVisibility =
                    ScrollBarVisibility.Disabled;
                scrollViewer1_SizeChanged(null, null);
            }
        }

        private void scrollViewer1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (image1.Stretch != Stretch.None)
            {
                image1.Width = scrollViewer1.ActualWidth;
                image1.Height = scrollViewer1.ActualHeight;
            }
        }

        private void scrollViewer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Return)
            {
                comboBox1.SelectedIndex = (comboBox1.SelectedIndex + 1) % comboBox1.Items.Count;
            }
            else if(e.Key == Key.Back)
                comboBox1.SelectedIndex = (comboBox1.SelectedIndex + comboBox1.Items.Count - 1) % 
                    comboBox1.Items.Count;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;
                rk.SetValue("Width", (int)ActualWidth);
                rk.SetValue("Height", (int)ActualHeight);
                rk.SetValue("DirList", (int)grid1.ColumnDefinitions[0].ActualWidth);
                rk.SetValue("FileList", (int)grid1.ColumnDefinitions[2].ActualWidth);
                rk.SetValue("Stretch", comboBox1.SelectedIndex);
                var dirInfo = (dirList1.SelectedItem as TreeViewItem).Tag as DirectoryInfo;
                rk.SetValue("Path", dirInfo == null ? "" : dirInfo.FullName);
                rk.SetValue("File", fileList1.SelectedIndex);
            }
            finally
            {
                if (rk != null)
                {
                    rk.Close();
                }
            }
        }
    }
}
