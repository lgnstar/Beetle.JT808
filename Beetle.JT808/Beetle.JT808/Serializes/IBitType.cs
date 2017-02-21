using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{



    public interface IBitCustomType
    {
        void Load(object value);

        object Save();
    }
}
