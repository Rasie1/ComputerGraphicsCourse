using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    interface IActor
    {
        void Update(Double dt, World world);
    }
}
