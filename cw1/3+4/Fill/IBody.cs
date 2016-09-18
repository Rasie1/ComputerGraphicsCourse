using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Engine
{
    interface IBody
    {
        Point Position { get; set; }

        void UpdatePhysics(Double dt, World world);
    }
}
