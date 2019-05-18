using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Drawing;
using System.Windows.Forms;

namespace laboratorium_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            OutputWriter.RemoveOutputFile();
            DataController.LinqStatements();
            DataController.PerformOperations();
            DataController.SortingAndSearchingWithCarBindingList();


            InitializeComponent();
            BindingList<Car> myCarsBindingList = new BindingList<Car>(DataController.myCars);
            BindingSource carBindingSource = new BindingSource();
            carBindingSource.DataSource = myCarsBindingList;
            //Drag a DataGridView control from the Toolbox to the form.
            dataGridView1.ItemsSource = carBindingSource;


        }
    }
}
