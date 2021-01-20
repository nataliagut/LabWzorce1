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
        public int colorPrice;
        public ContactLenses(string type)
        {
            _type = type;
            AdditionList = new List<IComposite>();
        }
    }
}
