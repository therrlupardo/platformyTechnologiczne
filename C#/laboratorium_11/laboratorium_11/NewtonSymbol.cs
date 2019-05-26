using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorium_11
{
    class NewtonSymbol
    {
        public int k { get; set; }
        public int n { get; set; }


        public NewtonSymbol(int n, int k)
        {
            this.n = n;
            this.k = k;
        }

        public double CalculateTasks()
        {
            if (n <= 0 || k <= 0) return -1;
            if (n < k) return -2;

            Task<double> counterTask = Task.Factory.StartNew(
                (obj) => CalculateCounter(),
                100 //state
                );

            Task<double> denominatorTask = Task.Factory.StartNew(
                (obj) => CalculateDenominator(),
                100 //state
                );

            counterTask.Wait();
            denominatorTask.Wait();
            return counterTask.Result / denominatorTask.Result;
        }

        public double CalculateDelegates()
        {
            if (n <= 0 || k <= 0) return -1;
            if (n < k) return -2;

            Func<double> counterFunc = CalculateCounter;
            Func<double> denominatorFunc = CalculateDenominator;

            IAsyncResult counter, denominator;
            counter = counterFunc.BeginInvoke(null, null);
            denominator = denominatorFunc.BeginInvoke(null, null);
            while (!counter.IsCompleted && !denominator.IsCompleted) ;

            return counterFunc.EndInvoke(counter) / denominatorFunc.EndInvoke(denominator);
        }


        public async Task<double> CalculateAsyncAwait()
        {
            var counter = Task.Run(() => CalculateCounter());
            var denominator =Task.Run(() => CalculateDenominator());

            await Task.WhenAll(counter, denominator);

            return counter.Result / denominator.Result;
        }



        private double CalculateCounter()
        {
            double returnValue = 1;
            for (int i = (n - k + 1); i <= n; i++)
            {
                returnValue *= i;
            }
            return returnValue;
        }
        private double CalculateDenominator()
        {
            double returnValue = 1;
            for (int i = 1; i <= k; i++)
            {
                returnValue *= i;
            }
            return returnValue;
        }

    }
}
