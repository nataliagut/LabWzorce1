using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    class ProgressiveBuilder : GlassesBuilder
    {
        public ProgressiveBuilder()
        {
            Glasses = new Glasses("Progressive");
        }
        public override void BuildLenses()
        {
            Glasses.Lenses = "Progressive";
            Glasses.LensesPrice = 150;
        }
    }
}
