using System.Numerics;

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
        public int Width { get; set; } = 40;
        public int Height { get; set; } = 40;

        private const float Gravity = 980;
        private bool hasDoubleJumped = false;
        private float highestY = 400; 

        public Player(string playerName)
        {
            Name = playerName;
            highestY = Position.Y;
        }

        public void Update(float deltaTime, List<Platform> platforms)
        {
            HandleInput();
            if (!IsGrounded) YVelocity += Gravity * deltaTime;
            Position = new Vector2(Position.X, Position.Y + YVelocity * deltaTime);
            if (Position.Y < highestY)
            {
                highestY = Position.Y;
                Score = Math.Max(0, (500 - highestY) / 10); 
            }

            IsGrounded = false;
            foreach (Platform platform in platforms)
            {
                if (platform.IsPlayerOnPlatform(this) && YVelocity >= 0)
                {
                    Position = new Vector2(Position.X, platform.Y - Height);
                    YVelocity = 0;
                    IsGrounded = true;
                    hasDoubleJumped = false;
                    break;
                }
            }

            if (Position.Y >= 500 - Height)
            {
                Position = new Vector2(Position.X, 500 - Height);
                YVelocity = 0;
                IsGrounded = true;
                hasDoubleJumped = false;
            }

            if (Position.X < 0)
                Position = new Vector2(0, Position.Y);
            else if (Position.X > 800 - Width)
                Position = new Vector2(800 - Width, Position.Y);

            if (Position.Y > 700)
            {
                Position = new Vector2(100, 400);
                YVelocity = 0;
                Score = Math.Max(0, Score - 10);
            }
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
            Raylib_cs.Raylib.DrawCircle((int)Position.X + 10, (int)Position.Y + 12, 3, Raylib_cs.Color.White);
            Raylib_cs.Raylib.DrawCircle((int)Position.X + 30, (int)Position.Y + 12, 3, Raylib_cs.Color.White);
            Raylib_cs.Raylib.DrawRectangle((int)Position.X + 15, (int)Position.Y + 25, 10, 3, Raylib_cs.Color.White);
            Raylib_cs.Raylib.DrawText(Name, (int)Position.X, (int)Position.Y - 20, 12, Raylib_cs.Color.Black);
        }
    }
}