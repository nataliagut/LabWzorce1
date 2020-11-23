using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public interface IComposite
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public void AddElement(IComposite element);
    }
}
