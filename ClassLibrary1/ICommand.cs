using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public interface ICommand
    {
        void Call();
        void Undo(); //funkcja cofa poprzednie polecenie.
    }
}
