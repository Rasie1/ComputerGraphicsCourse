using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PainterApplication
{
    public class NoTool : ITool
    {
        public void Down(Graphics target, Point pos)
        {
        }

        public void Up(Graphics target, Point pos)
        {
        }

        public void Move(Graphics target, Point pos)
        {

        }
    }
}
