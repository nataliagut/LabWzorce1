using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class ProgressiveBuilder : GlassProductBuilder
    {
        public ProgressiveBuilder(string product)
        {
            if (product == "glasses")
                Glasses = new Glasses("Progressive");
            else
                _ContactLenses = new ContactLenses("Progressive");
        }

        public override void BuildContactLenses()
        {
            _ContactLenses.Price += 5;
            _ContactLenses.cLColor = ContactLensesColorsEnum.Standard;
        }

        public override void BuildLenses()
        {
            Glasses.Lenses = "Progressive";
            Glasses.LensesPrice = 150;
            Glasses.Price += 150;
        }
    }
}