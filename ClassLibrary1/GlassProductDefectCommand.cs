using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class GlassProductDefectCommand : ICommand
    {
        private GlassProduct _glassProduct;
        WhatToDoEnum _whatToDo;

        private IComposite _element;

        public void DefectCmd(GlassProduct glassProduct, WhatToDoEnum whattodo, IComposite element)
        {
            _glassProduct = glassProduct;
            _whatToDo = whattodo;
            _element = element;
        }
        public void Call()
        {
            switch (_whatToDo)
            {
                case WhatToDoEnum.AddElement:
                    _glassProduct.AddElement(_element);
                    break;
                case WhatToDoEnum.RemoveElement:
                    _glassProduct.RemoveElement(_element);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public void Undo()
        {
            switch (_whatToDo)
            {
                case WhatToDoEnum.AddElement:
                    _glassProduct.RemoveElement(_element);
                    break;
                case WhatToDoEnum.RemoveElement:
                    _glassProduct.AddElement(_element);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
