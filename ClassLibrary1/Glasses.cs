using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Glasses
    {
        public string _type;
        public string Lenses { get; set; }
        public float LensesPrice { get; set; }

        public string Rims { get; set; }
        public float RimsPrice { get; set; }
        public double Price { get; set; }
        public List<IComposite> AdditionList { get; set; }
        public Glasses(string glassesType)
        {
            _type = glassesType;
            Price = 0;
            Rims = "standard";
            RimsPrice = 100;
            AdditionList = new List<IComposite>();
        }
        public void Display()
        {
            Console.WriteLine("typ:" + _type);
            Console.WriteLine("lenses:" + Lenses);
            Console.WriteLine("lenses price:" + LensesPrice);
            Console.WriteLine("Rims:" + Rims);
            Console.WriteLine("Rims price:" + RimsPrice);
        }
        
    }
}
