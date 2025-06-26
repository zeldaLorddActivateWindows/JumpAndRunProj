using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    using Raylib_cs;
    internal class main : IGameWIndow
    {
        public string Name { get; } = "Jump and Run";
        public int Width { get; } = 300;
        public int Height { get; } = 1000;

        public void Update() { }
        public void Exit() { }

        internal static void Main(string[] args)
        {

        }
    }
}
