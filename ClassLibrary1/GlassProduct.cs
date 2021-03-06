﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class GlassProduct : IComposite
    {
        public List<IComposite> AddedProducts;
        public string Name { get; set; }
        public double Price { get; set; }
        public GlassProduct()
        {
            AddedProducts = new List<IComposite>();
        }
        public void AddElement(IComposite element)
        {
            AddedProducts.Add(element);
        }
        public void RemoveElement(IComposite element)
        {
            string name = element.Name;
            AddedProducts.RemoveAll(el => el.Name == name);
        }
        public void ShowList()
        {
            foreach (IComposite el in AddedProducts)
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
        public void AddToContactLenses(ContactLenses CL)
        {
            foreach (IComposite el in AddedProducts)
                CL.AdditionList.Add(el);
        }
    }
}