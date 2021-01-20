using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class MultifocalBuilder : GlassProductBuilder
    {
        public MultifocalBuilder(string product)
        {
            if (product == "glasses")
                Glasses = new Glasses("Multifocal");
            else
                _ContactLenses = new ContactLenses("Multifocal");
        }

        public override void BuildContactLenses()
        {
            _ContactLenses.Price += 4;
            _ContactLenses.cLColor = ContactLensesColorsEnum.Standard;
        }

        public override void BuildLenses()
        {
            Glasses.Lenses = "Multifocal";
            Glasses.LensesPrice = 200;
            Glasses.Price += 200;
        }
    }
}