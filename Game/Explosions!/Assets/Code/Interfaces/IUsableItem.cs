using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Interfaces
{
    public interface IUsableItem
    {
        bool Use();

        //TODO: may want to support activating different uses on the same item
        //public bool Use(int mode);
    }
}
