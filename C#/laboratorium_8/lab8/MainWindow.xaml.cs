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

namespace lab8
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

        private void Open(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog()
            {
                Description = "Select directory to open"
            };
            DialogResult result = dlg.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                treeView.Items.Clear();
                var parts = dlg.SelectedPath.Split('\\');
                var root = new TreeViewItem
                {
                    Header = parts[parts.Length-1],
                    Tag = dlg.SelectedPath
                };

                DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath);
                foreach(DirectoryInfo subdir in dir.GetDirectories())
                {
                    root.Items.Add(this.MakeTreeDirectory(subdir));
                }
                foreach(FileInfo file in dir.GetFiles())
                {
                    root.Items.Add(this.MakeTreeFile(file));
                }
                treeView.Items.Add(root);
            }
        }

        private object MakeTreeFile(FileInfo file)
        {
            var item = new TreeViewItem
            {
                Header = file.Name,
                Tag = file.Name
            };
            return item;
        }

        private object MakeTreeDirectory(DirectoryInfo dir)
        {
            var root = new TreeViewItem
            {
                Header = dir.Name,
                Tag = dir.Name
            };

            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                root.Items.Add(this.MakeTreeDirectory(subdir));
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                root.Items.Add(this.MakeTreeFile(file));
            }
            return root;
        }

            private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
