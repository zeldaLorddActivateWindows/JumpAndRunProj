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
    internal interface IPowerup
    {
        Point Position { get; }
        string Name { get; }
        float DoubleSpeed {  get; } 
        float Dash { get; }
        bool IsDashing { get; }

    }
}
