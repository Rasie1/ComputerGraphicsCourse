using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Engine
{
    interface IRenderable
    {
        void Render(Graphics target);
    }
}
