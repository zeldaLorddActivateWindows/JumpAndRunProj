using System.Numerics;

namespace JumpAndRun
{
    internal interface IPowerup
    {
        Vector2 Position { get; }
        int Width { get; }
        int Height { get; }
        bool IsCollected { get; }
        void Draw();
        bool CheckCollision(Player player);
        void OnCollision(Player player);
    }
}