

namespace JumpAndRun
{
    using Raylib_cs;
    using System.Runtime.InteropServices;

    internal class main : IGameWindow
    {
        private int maxPlatforms = 15;
        private List<Platform> platforms = new List<Platform>();
        private float lastPlatformY = 400;
        private Random random = new Random();

        public string NAME { get; } = "Jump and Run";
        public int WIDTH { get; } = 800;
        public int HEIGHT { get; } = 600;
        public int FPS { get; } = 60;
        private bool isRunning = true;
        private List<KeyboardKey> ExitKeys = new List<KeyboardKey>() { KeyboardKey.Escape };
        private Player player;

        internal static void Main(string[] args)
        {
            main game = new main();
            game.Init();

            while (!Raylib.WindowShouldClose() && game.isRunning)
            {
                game.Update();
                game.Draw();
            }
            game.Exit();
        }

        public void Init()
        {
            Raylib.InitWindow(WIDTH, HEIGHT, NAME);
            Raylib.SetTargetFPS(FPS);
            player = new Player("pexlover");
            GenerateInitialPlatforms();
        }

        private void GenerateInitialPlatforms()
        {
            platforms.Add(new Platform(200, 450, 120));
            platforms.Add(new Platform(400, 380, 100));
            platforms.Add(new Platform(100, 320, 80));
            platforms.Add(new Platform(550, 300, 90));
            platforms.Add(new Platform(300, 240, 110));

            lastPlatformY = 240;
        }

        public void Update()
        {
            foreach (var key in ExitKeys)
            {
                if (Raylib.IsKeyPressed(key))
                {
                    isRunning = false;
                    break;
                }
            }

            float deltaTime = Raylib.GetFrameTime();
            player.Update(deltaTime, platforms);
            GeneratePlatforms();
            CleanupPlatforms();
        }

        private void GeneratePlatforms()
        {
            while (platforms.Count < maxPlatforms)
            {
                float newY = lastPlatformY - random.Next(60, 120); 
                float newX = random.Next(0, WIDTH - 150);
                float newWidth = random.Next(80, 150);
                if (newX + newWidth > WIDTH) newX = WIDTH - newWidth;
                platforms.Add(new Platform(newX, newY, newWidth));
                lastPlatformY = newY;
            }
        }

        private void CleanupPlatforms()
        {
            float playerY = player.Position.Y;
            platforms.RemoveAll(platform => platform.Y > playerY + 300);
            if (platforms.Count < maxPlatforms) GeneratePlatforms();
        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);
            Raylib.DrawRectangle(0, 500, WIDTH, HEIGHT - 500, Color.Green);
            foreach (Platform platform in platforms) platform.Draw();
            player.Draw();
            Raylib.DrawText("Controls:", 10, 10, 16, Color.Black);
            Raylib.DrawText("A/D or Arrow Keys to move", 10, 30, 12, Color.Black);
            Raylib.DrawText("Space/W/Up to Jump", 10, 45, 12, Color.Black);
            Raylib.DrawText("ESC to Exit", 10, 60, 12, Color.Black);

            string groundedText = player.IsGrounded ? "Grounded" : "Airborne";
            string doubleJumpText = player.CanDoubleJump && !player.IsGrounded ? "Double Jump Available" : "Double Jump Used";

            Raylib.DrawText($"Status: {groundedText}", 10, HEIGHT - 80, 12, Color.Black);
            if (!player.IsGrounded)
            {
                Raylib.DrawText($"{doubleJumpText}", 10, HEIGHT - 65, 12, Color.Black);
            }
            Raylib.DrawText($"Score: {player.Score:F0}", 10, HEIGHT - 50, 16, Color.Red);
            Raylib.DrawText($"Height: {Math.Max(0, (500 - player.Position.Y)):F0}m", 10, HEIGHT - 30, 12, Color.Black);
            Raylib.DrawText($"Platforms: {platforms.Count}", 10, HEIGHT - 15, 10, Color.Gray);

            Raylib.EndDrawing();
        }

        public void Exit() => Raylib.CloseWindow();
    }
}
