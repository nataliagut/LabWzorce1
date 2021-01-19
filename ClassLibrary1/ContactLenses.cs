using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class ContactLenses
    {
        public string _type;
        public double Price { get; set; }
        public List<IComposite> AdditionList { get; set; }
        public ContactLensesColorsEnum cLColor;
        public ContactLenses(string type)
        {
            _type = type;
            AdditionList = new List<IComposite>();
        }
        public void Display()
        {
            Console.WriteLine("type: " + _type);
            Console.WriteLine("color: " + cLColor.ToString());
            foreach (IComposite el in AdditionList)
            {
                Console.WriteLine(el.Name + " " + el.Price);
            }
            Console.WriteLine("Total price:" + Price);
        }
    }
}
