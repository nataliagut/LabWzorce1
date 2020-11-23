using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class GlassProduct : IComposite
    {
        public List<IComposite> AddedProducts;
        public string Name { get; set; }
        public float Price { get; set; }
        public GlassProduct(Glasses glasses)
        {
            //AddedProducts = glasses.AdditionList;
        }
        public void AddElement(IComposite element)
        {
            AddedProducts.Add(element);
        }
        public void ShowList()
        {
            foreach(IComposite el in AddedProducts)
            {
                Console.WriteLine(el.Name);
                Console.WriteLine(el.Price);
            }
        }
        public void AddToGlasses(Glasses glasses)
        {
            foreach (IComposite el in AddedProducts)
            {
                glasses.AdditionList.Add(el);
            }
        }
    }
}
