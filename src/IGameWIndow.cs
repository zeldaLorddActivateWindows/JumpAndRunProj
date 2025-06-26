using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    internal interface IGameWIndow
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }

        public void Update();
        public void Exit();
    }
}
