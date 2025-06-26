using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    internal class Player
    {
        string Name { get; set; } = string.Empty;
        float Score { get; set; } = 0;
        float XVelocity { get; set; } = 10;
        int JumpHeight { get; set; } = 10;
        Vector2 Position { get; set; } = new Vector2(0, 0);
        public bool IsGrounded { get; set; } = false;
        public bool CanDoubleJump { get; set; } = true;

        public Player(string eName, float eScore) 
        {
            eName = Name;
            eScore = Score;
            
        }



    }
}
