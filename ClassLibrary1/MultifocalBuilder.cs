using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    class MultifocalBuilder : GlassesBuilder
    {
        public MultifocalBuilder()
        {
            Glasses = new Glasses("Multifocal");
        }
        public override void BuildLenses()
        {
            Glasses.Lenses = "Multifocal";
            Glasses.LensesPrice = 200;
        }
    }
}
