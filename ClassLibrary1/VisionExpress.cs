
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class VisionExpress
    {
        public void ConstructGlasses(GlassProductBuilder glassBuilder)
        {
            glassBuilder.BuildLenses();
        }
        public void ConstructContactLenses(GlassProductBuilder cLBuilder)
        {
            cLBuilder.BuildContactLenses();
        }
    }
}