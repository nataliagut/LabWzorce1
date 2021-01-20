using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class AntireflectiveBuilder : GlassProductBuilder
    {
        public AntireflectiveBuilder(string product)
        {
            if (product == "glasses")
                Glasses = new Glasses("Antireflective");
            else
                _ContactLenses = new ContactLenses("Antireflective");
        }

        public override void BuildContactLenses()
        {
            _ContactLenses.Price += 6;
            _ContactLenses.cLColor = ContactLensesColorsEnum.Standard;
        }

        public override void BuildLenses()
        {
            Glasses.Lenses = "Antireflective";
            Glasses.LensesPrice = 100;
            Glasses.Price += 100;
        }

    }
}