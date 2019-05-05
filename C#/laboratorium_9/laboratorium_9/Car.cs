using System;
using System.Collections.Generic;
using System.Text;

namespace laboratorium_9
{
    public class Car
    {
        public string model;
        public int year;
        public Engine motor;

        public Car(string model, Engine motor, int year)
        {
            this.model = model;
            this.motor = motor;
            this.year = year;
        }
    }
}
