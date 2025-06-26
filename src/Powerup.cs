using JumpAndRun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    record struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y) { X = x; Y = y; }
    }
    internal class Powerup
    {
        Point Position { get; set; }
        string Name {  get; set; }
        float DoubleSpeed {  get; set; } 
        float Dash {  get; set; }
        bool IsDashing { get; set; } = false;

        public Powerup()
        {

        }

    }
}
