using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Filters : IComposite
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Filters(FiltersEnum name)
        {
            Name = name + " filter";
            switch (name)
            {
                case FiltersEnum.ForComputer:
                    Price = 300;
                    break;
                case FiltersEnum.UV:
                    Price = 400;
                    break;
                case FiltersEnum.Polarized:
                    Price = 200;
                    break;
            }
        }
        public void AddElement(IComposite el)
        {
        }
    }
}