using System.Numerics;

namespace JumpAndRun
{
    internal class PowerupMultiplier : IPowerup
    {
        public Vector2 Position { get; private set; }
        public int Width { get; } = 20;
        public int Height { get; } = 20;
        public bool IsCollected { get; private set; } = false;

        public PowerupMultiplier(Vector2 position)
        {
            Position = position;
        }

        public PowerupMultiplier(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        public void Draw()
        {
            if (!IsCollected)
            {
                Raylib_cs.Raylib.DrawRectangle((int)Position.X, (int)Position.Y, Width, Height, Raylib_cs.Color.Gold);
                Raylib_cs.Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, Width, Height, Raylib_cs.Color.Yellow);
                Raylib_cs.Raylib.DrawText("x1.2", (int)Position.X + 2, (int)Position.Y + 2, 12, Raylib_cs.Color.Black);
            }
        }

        public bool CheckCollision(Player player)
        {
            return !IsCollected &&
                   player.Position.X < Position.X + Width &&
                   player.Position.X + player.Width > Position.X &&
                   player.Position.Y < Position.Y + Height &&
                   player.Position.Y + player.Height > Position.Y;
        }

        public void OnCollision(Player player)
        {
            if (!IsCollected)
            {
                IsCollected = true;
                player.ApplyScoreMultiplier(1.2f);
            }
        }
    }
}