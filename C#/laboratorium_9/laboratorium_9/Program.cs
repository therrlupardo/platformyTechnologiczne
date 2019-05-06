using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace laboratorium_9
{
    class Program
    {
        private static readonly List<Car> myCars = new List<Car>
            {
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };

        static void Main()
        {
            Exercise1();
            Exercise2();
        }

        private static void Exercise2()
        {
            var fileName = "cars.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, fileName);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                XElement xml = new XElement("cars", myCars
                .Select(n => new XElement("car",
                new XElement("model", n.model),
                new XElement("engine",
                    new XAttribute("model", n.motor.model),
                    new XElement("displacement", n.motor.displacement),
                    new XElement("horsePower", n.motor.horsePower)),
                new XElement("year", n.year))));
                sw.Write(xml.ToString());
            }

        }

        private static void Exercise1()
        {
            Console.WriteLine("Zadanie 1a - Projekcja myCar na typ anonimowy:");
            var myCarToAnonymousTypeQuery = myCars
                                                .Where(s => s.model.Equals("A6"))
                                                .Select(car =>
                                                    new
                                                    {
                                                        engineType = String.Compare(car.motor.model, "TDI") == 0
                                                                   ? "diesel"
                                                                   : "petrol",
                                                        hppl = car.motor.horsePower / car.motor.displacement,
                                                    });
            foreach (var elem in myCarToAnonymousTypeQuery)
            {
                Console.WriteLine(elem.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Zadanie 1b - grupowanie wartości hppl po typie silnika");
            var groupedQuery = myCarToAnonymousTypeQuery
                .GroupBy(elem => elem.engineType)
                .Select(elem => $"{elem.First().engineType}: {elem.Average(s => s.hppl).ToString()}");
            foreach (var elem in groupedQuery)
            {
                Console.WriteLine(elem);
            }
            Console.WriteLine();
        }


    }
}
