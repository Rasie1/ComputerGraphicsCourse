using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PainterApplication
{
    interface ITool
    {
        void Down(Graphics target, Point pos);
        void Up(Graphics target, Point pos);
        void Move(Graphics target, Point pos);
    }
}
