using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace laboratorium_11
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NewtonSymbol newtonSymbol;
        private int highestPercentageReached;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonClick_NetwonSymbolTasks(object sender, RoutedEventArgs e)
        {
            int k, n;
            if (!Int32.TryParse(textBox_n.Text, out n) || !Int32.TryParse(textBox_k.Text, out k))
            {
                SetErrorLabel("Aby obliczyć wartość Symbolu Newtona musisz najpierw ustawić n i k!");
                return;
            }
            newtonSymbol = new NewtonSymbol(n, k);
            double result = newtonSymbol.CalculateTasks();
            switch (result)
            {
                case -1:
                    SetErrorLabel("n i k muszą być większe od 0!");
                    break;
                case -2:
                    SetErrorLabel("k nie może być większe niż n!");
                    break;
                default:
                    textBox_tasks.Text = result.ToString();
                    SetErrorLabel("");
                    break;
            }
            
        }
        private void ButtonClick_NetwonSymbolDelegates(object sender, RoutedEventArgs e)
        {
            int k, n;
            if (!Int32.TryParse(textBox_n.Text, out n) || !Int32.TryParse(textBox_k.Text, out k))
            {
                SetErrorLabel("Aby obliczyć wartość Symbolu Newtona musisz najpierw ustawić n i k!");
                return;
            }
            newtonSymbol = new NewtonSymbol(n, k);
            double result = newtonSymbol.CalculateDelegates();
            switch (result)
            {
                case -1:
                    SetErrorLabel("n i k muszą być większe od 0!");
                    break;
                case -2:
                    SetErrorLabel("k nie może być większe niż n!");
                    break;
                default:
                    textBox_delegates.Text = result.ToString();
                    SetErrorLabel("");
                    break;
            }
        }
        private async void ButtonClick_NetwonSymbolAsyncAwait(object sender, RoutedEventArgs e)
        {
            int k, n;
            if (!Int32.TryParse(textBox_n.Text, out n) || !Int32.TryParse(textBox_k.Text, out k))
            {
                SetErrorLabel("Aby obliczyć wartość Symbolu Newtona musisz najpierw ustawić n i k!");
                return;
            }
            newtonSymbol = new NewtonSymbol(n, k);
            double result = await newtonSymbol.CalculateAsyncAwait();
            switch (result)
            {
                case -1:
                    SetErrorLabel("n i k muszą być większe od 0!");
                    break;
                case -2:
                    SetErrorLabel("k nie może być większe niż n!");
                    break;
                default:
                    textBox_async_await.Text = result.ToString();
                    SetErrorLabel("");
                    break;
            }

        }
        private void ButtonClick_Get(object sender, RoutedEventArgs e)
        {
            int i;
            if(!Int32.TryParse(textBox_i.Text, out i))
            {
                SetErrorLabel("Aby obliczyć i-ty element ciągu fibonacciego musisz podać i!");
            }
            BackgroundWorker fibonacciWorker = new BackgroundWorker();
            fibonacciWorker.DoWork += new DoWorkEventHandler(fibonacciWorker_DoWork);
            fibonacciWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(fibonacciWorker_RunWorkerCompleted);
            fibonacciWorker.ProgressChanged += new ProgressChangedEventHandler(fibonacciWorker_ProgressChanged);
            highestPercentageReached = 0;
            progressBar.Value = 0;
            fibonacciWorker.RunWorkerAsync(i);
        }

        private void fibonacciWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.WorkerReportsProgress = true;
            e.Result = ComputeFibonacci((int)e.Argument, worker, e);
        }

        private void fibonacciWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox_fibonacci.Text = e.Result.ToString();
        }

        private void fibonacciWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private UInt64 ComputeFibonacci(int n, BackgroundWorker worker, DoWorkEventArgs e)
        {
            if(n < 0)
            {
                SetErrorLabel("Ciąg Fibonacciego można policzyć tylko dla i > 0!");
                return 0;
            }
            UInt64 result = 0;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                List<UInt64> listOfFibonacciElements = new List<UInt64>();
                
                for(int i = 1; i <= n; i++)
                {
                    if (i <= 2)
                    {
                        listOfFibonacciElements.Add(1);
                    }
                    else
                    {
                        var a = listOfFibonacciElements.Last();
                        listOfFibonacciElements.Remove(a);
                        var b = listOfFibonacciElements.Last();
                        listOfFibonacciElements.Add(a);
                        listOfFibonacciElements.Add(a + b);
                    }
                    int percentComplete = (int)((float)i / n * 100);
                    if (percentComplete > highestPercentageReached)
                    {
                        highestPercentageReached = percentComplete;
                        worker.ReportProgress(percentComplete);
                        Thread.Sleep(20);
                    }
                }
                result = listOfFibonacciElements.Last();
            }
            

            return result;
        }

       

        private void ButtonClick_Resolve(object sender, RoutedEventArgs e)
        {
            var domainList = DomainConverter.ConvertDomains();
            textBox_output.Text = "";
            foreach(var domain in domainList)
            {
                textBox_output.Text += $"{domain.Item1} => {domain.Item2}\n";
            }

        }
        private void ButtonClick_Compress(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Description = "Select directory to compress"
            };
            DialogResult result = dialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dialog.SelectedPath);
                Compresser.CompressDirectory(directoryInfo);
            }

        }
        private void ButtonClick_Decompress(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog()
            {
                Description = "Select directory to decompress"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dialog.SelectedPath);
                Compresser.DecompressDirectory(directoryInfo);
            }
        }
        private void LabelDoubleClick_ClearErrorLabel(object sender, RoutedEventArgs e)
        {
            SetErrorLabel("");
        }

        private void SetErrorLabel(string error)
        {
            label_error.Content = error;
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
