using System;
using System.Collections.Generic;
using System.Text;

namespace laboratorium_9
{
    public class Engine
    {
        public double displacement;
        public double horsePower;
        public string model;

        public Engine(double displacement, double horsePower, string model)
        {
            this.displacement = displacement;
            this.horsePower = horsePower;
            this.model = model;
        }

    }
}
