using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    internal class Platform
    {
        float X { get; set; }
        float Y { get; set; }
        float Width { get; set; }
        float Height { get; set; } = 10f;
        public Platform(float eX, float eY, float eWidth)
        {
            eX = X;
            eY = Y;
            eWidth = Width;
        }


    }
}
