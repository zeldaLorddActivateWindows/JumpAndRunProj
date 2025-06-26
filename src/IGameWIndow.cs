using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    internal interface IGameWindow
    {
        public string NAME { get; }
        public int WIDTH { get; }
        public int HEIGHT { get; }
        public int FPS { get; }
        public void Init();
        public void Update();
        public void Exit();
    }
}