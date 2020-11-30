using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public interface IComposite
    {
        public string Name { get; }
        public double Price { get; }
        public void AddElement(IComposite element);
    }
}