using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laboratorium_8
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFolderBrowserDialog(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };
            var result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = dlg.SelectedPath;
                var root = new TreeViewItem();
                if (File.Exists(path))
                {
                    FileInfo file = new FileInfo(path);

                    root = new TreeViewItem
                    {
                        Header = file.Name,
                        Tag = path
                    };
                }
                else if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    root = AddSubfiles(dir);                    
                }
                


            }
            

        }

        private TreeViewItem AddSubfiles(DirectoryInfo dir)
        {
            var root = new TreeViewItem
            {
                Header = dir.Name,
                Tag = dir.FullName
            };
            foreach (FileInfo file in dir.GetFiles())
            {
                var item = new TreeViewItem
                {
                    Header = file.Name,
                    Tag = dir.FullName + @"\" + file.Name
                };
                root.Items.Add(item);
            }
            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                root.Items.Add(AddSubfiles(dir));
            }
            return root;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
