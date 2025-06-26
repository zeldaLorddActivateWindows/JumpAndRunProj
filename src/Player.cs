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
        public string Name { get; set; }
        public float Score { get; set; } = 0;
        public float XVelocity { get; set; } = 200; 
        public float YVelocity { get; set; } = 0;
        public float JumpStrength { get; set; } = 400; 
        public Vector2 Position { get; set; } = new Vector2(100, 400);
        public bool IsGrounded { get; set; } = false;
        public bool CanDoubleJump { get; set; } = true;
        public int Width { get; set; } = 50;
        public int Height { get; set; } = 50;

        private const float Gravity = 980; 
        private const float GroundY = 500; 
        private bool hasDoubleJumped = false;

        public Player(string playerName)
        {
            Name = playerName;
        }

        public void Update(float deltaTime)
        {
            HandleInput();
            if (!IsGrounded) YVelocity += Gravity * deltaTime;
            Position = new Vector2(Position.X, Position.Y + YVelocity * deltaTime);
            if (Position.Y >= GroundY - Height)
            {
                Position = new Vector2(Position.X, GroundY - Height);
                YVelocity = 0;
                IsGrounded = true;
                hasDoubleJumped = false;
            }
            else IsGrounded = false;
            

            if (Position.X < 0) Position = new Vector2(0, Position.Y);
            else if (Position.X > 800 - Width) Position = new Vector2(800 - Width, Position.Y);
        }

        private void HandleInput()
        {
            float deltaTime = Raylib_cs.Raylib.GetFrameTime();

            if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.A) || Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Left)) Position = new Vector2(Position.X - XVelocity * deltaTime, Position.Y);
            if (Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.D) || Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.Right)) Position = new Vector2(Position.X + XVelocity * deltaTime, Position.Y);
            if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Space) || Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.W) || Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.Up))
            {
                if (IsGrounded)
                {
                    YVelocity = -JumpStrength;
                    IsGrounded = false;
                }
                else if (CanDoubleJump && !hasDoubleJumped)
                {
                    YVelocity = -JumpStrength;
                    hasDoubleJumped = true;
                }
            }
        }

        public void Draw()
        {
            Raylib_cs.Raylib.DrawRectangle((int)Position.X, (int)Position.Y, Width, Height, Raylib_cs.Color.Blue);
            Raylib_cs.Raylib.DrawCircle((int)Position.X + 12, (int)Position.Y + 15, 3, Raylib_cs.Color.White);
            Raylib_cs.Raylib.DrawCircle((int)Position.X + 38, (int)Position.Y + 15, 3, Raylib_cs.Color.White);
            Raylib_cs.Raylib.DrawText(Name, (int)Position.X, (int)Position.Y - 20, 12, Raylib_cs.Color.Black);
        }
    }
}

