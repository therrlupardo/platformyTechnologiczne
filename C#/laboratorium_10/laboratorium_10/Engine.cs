using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace laboratorium_10
{
    public class Engine
    {
        public double displacement { get; set; }

        public int horsePower { get; set; }

        public string model { get; set; }


        public Engine() { }
        public Engine(double displacement, int horsePower, string model)
        {
            this.displacement = displacement;
            this.horsePower = horsePower;
            this.model = model;
        }

        public override string ToString()
        {
            return $"{model} {displacement} ({horsePower} hp)"; 
        }


    }
}
