using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary1
{
    public class Glasses
    {
        public string _type;
        public string Lenses { get; set; }
        public double LensesPrice { get; set; }
        public string Rims { get; set; }
        public double RimsPrice { get; set; }
        public double Price { get; set; }

        public List<IComposite> AdditionList { get; set; }
        public Glasses(string glassesType)
        {
            _type = glassesType;
            Price = 100;
            Rims = "standard";
            RimsPrice = 100;
            AdditionList = new List<IComposite>();
        }
        public void Display()
        {
            Console.WriteLine("type:" + _type);
            Console.WriteLine("lenses:" + Lenses);
            Console.WriteLine("lenses price:" + LensesPrice);
            Console.WriteLine("Rims:" + Rims);
            Console.WriteLine("Rims price:" + RimsPrice);
            foreach (IComposite el in AdditionList)
            {
                Console.WriteLine(el.Name + " " + el.Price);
                //Console.WriteLine(el.Price);
            }
            Console.WriteLine("Total price:" + (LensesPrice + RimsPrice + Price));
        }

    }
}