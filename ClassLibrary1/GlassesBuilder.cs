using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public abstract class GlassesBuilder
    {
        public Glasses Glasses { get; set; }
        //public abstract void getGlassesType();
        //public abstract void getRims();
        public abstract void BuildLenses();

    }
}