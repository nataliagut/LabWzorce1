using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class UVFilter : IComposite
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public UVFilter()
        {
            Name = "UV filter";
            Price = 100;
        }
        public void AddElement(IComposite el)
        { 
        }
    }
}
