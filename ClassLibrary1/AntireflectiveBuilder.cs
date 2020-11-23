using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    class AntireflectiveBuilder : GlassesBuilder
    {
        public AntireflectiveBuilder()
        {
            Glasses = new Glasses("Antireflective");
        }
            public override void BuildLenses()
        {
            Glasses.Lenses = "Antireflective";
            Glasses.LensesPrice = 100;
        }
        
    }
}
