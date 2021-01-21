using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class DefectValue : IComposite
    {
        public string Name { get; }
        public double Price { get; }
        public DefectValue(double left, double right)
        {
            Name = $"Left defect: {left}, Right defect: {right}";
            if (left <= -10)
                Price += 300;
            else if (left > -10 && left < -3)
                Price += 200;
            else if (left > 3 && left < 10)
                Price += 200;
            else if (left >= 10)
                Price += 300;
            else
                Price += 100;

            if (right <= -10)
                Price += 300;
            else if (right > -10 && right < -3)
                Price += 200;
            else if (right > 3 && right < 10)
                Price += 200;
            else if (right >= 10)
                Price += 300;
            else
                Price += 100;
        }
        public void AddElement(IComposite el)
        {
        }
    }
}