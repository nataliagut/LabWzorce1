using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public abstract class GlassProductBuilder
    {
        public Glasses Glasses { get; set; }
        public ContactLenses _ContactLenses { get; set; }
        public abstract void BuildLenses();
        public abstract void BuildContactLenses();
    }
}