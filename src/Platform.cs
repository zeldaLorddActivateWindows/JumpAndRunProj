namespace JumpAndRun
{
    internal class Platform
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; } = 15f;
        public Raylib_cs.Color Color { get; set; } = Raylib_cs.Color.Brown;

        public Platform(float x, float y, float width)
        {
            X = x;
            Y = y;
            Width = width;
        }

        public void Draw()
        {
            Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, (int)Width, (int)Height, Color);
            Raylib_cs.Raylib.DrawRectangleLines((int)X, (int)Y, (int)Width, (int)Height, Raylib_cs.Color.DarkBrown);
        }

        public bool IsPlayerOnPlatform(Player player)
        {
            return player.Position.X + player.Width > X &&
                   player.Position.X < X + Width &&
                   player.Position.Y + player.Height >= Y &&
                   player.Position.Y + player.Height <= Y + Height + 5; 
        }
    }
}

