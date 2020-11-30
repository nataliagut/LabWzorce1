using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Rims : IComposite
    {
        public string Name { get; }
        public double Price { get; }
        public Rims(RimTypesEnum name)
        {
            Name = name + " rims";
            switch (name)
            {
                case RimTypesEnum.Armani:
                    Price = 500;
                    break;
                case RimTypesEnum.Dior:
                    Price = 600;
                    break;
                case RimTypesEnum.Gucci:
                    Price = 550;
                    break;
            }
        }
        public void AddElement(IComposite el)
        {
        }
    }
}