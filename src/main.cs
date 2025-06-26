namespace JumpAndRun
{
    using Raylib_cs;
    using System.Runtime.InteropServices;

    internal class main : IGameWindow
    {
        private int maxPlatforms = 100;
        private List<Platform> platforms = new List<Platform>();
        private float lastPlatformY = 400;
        private Random random = new Random();
        public string NAME { get; } = "Jump and Run";
        public int WIDTH { get; } = 800;
        public int HEIGHT { get; } = 600;
        public int FPS { get; } = 60;
        private bool isRunning = true;
        private bool isGameOver = false;
        private List<KeyboardKey> ExitKeys = new List<KeyboardKey>() { KeyboardKey.Escape };
        private List<KeyboardKey> RestartKeys = new List<KeyboardKey>() { KeyboardKey.R };
        private Player player;
        private Camera2D camera;
        private float deathTimer = 0f;
        private const float DEATH_DELAY = 2f;
        private bool isDying = false;
        private float lastValidY = 400f;
        private const float GROUND_LEVEL = 500f;
        private const float DEATH_BOUNDARY = 700f;

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
            camera = new Camera2D();
            camera.Target = new System.Numerics.Vector2(WIDTH / 2, HEIGHT / 2);
            camera.Offset = new System.Numerics.Vector2(WIDTH / 2, HEIGHT / 2);
            camera.Rotation = 0.0f;
            camera.Zoom = 1.0f;
            player = new Player("pexlover");
            GenerateInitialPlatforms();
            lastValidY = player.Position.Y;
        }

        private void GenerateInitialPlatforms()
        {
            platforms.Clear();
            platforms.Add(new Platform(200, 450, 120));
            platforms.Add(new Platform(400, 380, 100));
            platforms.Add(new Platform(100, 320, 80));
            platforms.Add(new Platform(550, 300, 90));
            platforms.Add(new Platform(300, 240, 110));
            platforms.Add(new Platform(150, 180, 100));
            platforms.Add(new Platform(500, 120, 80));
            lastPlatformY = 120;
        }

        public void Update()
        {
            foreach (var key in ExitKeys)
            {
                if (Raylib.IsKeyPressed(key))
                {
                    isRunning = false;
                    return;
                }
            }

            if (isGameOver)
            {
                foreach (var key in RestartKeys)
                {
                    if (Raylib.IsKeyPressed(key))
                    {
                        ResetGame();
                        return;
                    }
                }
                return;
            }

            float deltaTime = Raylib.GetFrameTime();

            if (isDying)
            {
                deathTimer += deltaTime;
                if (deathTimer >= DEATH_DELAY)
                {
                    isGameOver = true;
                    isDying = false;
                    deathTimer = 0f;
                }
                return;
            }

            player.Update(deltaTime, platforms);
            maxPlatforms++;
            if (player.Position.Y > DEATH_BOUNDARY)
            {
                isDying = true;
                deathTimer = 0f;
                return;
            }

            if (player.Position.Y > lastValidY + HEIGHT * 1.5f && player.YVelocity > 0)
            {
                isDying = true;
                deathTimer = 0f;
                return;
            }

            camera.Target.Y = Math.Min(player.Position.Y, lastValidY);

            if (player.Position.Y < lastValidY - 50)
            {
                lastValidY = player.Position.Y;
                CleanupPlatforms();
                GeneratePlatforms();
            }
        }

        private void ResetGame()
        {
            isGameOver = false;
            isDying = false;
            deathTimer = 0f;
            platforms.Clear();
            player = new Player("pexlover");
            GenerateInitialPlatforms();
            lastValidY = player.Position.Y;
            camera.Target.Y = player.Position.Y;
        }

        private void GeneratePlatforms()
        {
            while (platforms.Count < maxPlatforms)
            {
                float newY = lastPlatformY - random.Next(60, 120);
                float newX = random.Next(50, WIDTH - 200);
                float newWidth = random.Next(80, 150);
                newX = Math.Max(0, Math.Min(newX, WIDTH - newWidth));
                platforms.Add(new Platform(newX, newY, newWidth));
                lastPlatformY = newY;
            }
        }

        private void CleanupPlatforms()
        {
            float cleanupThreshold = Math.Max(player.Position.Y + HEIGHT * 2, lastValidY + HEIGHT * 2);
            platforms.RemoveAll(platform => platform.Y > cleanupThreshold);
            if (platforms.Count < maxPlatforms) GeneratePlatforms();
        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);

            if (!isGameOver)
            {
                Raylib.BeginMode2D(camera);
                Raylib.DrawRectangle(0, (int)camera.Target.Y + 500, WIDTH, HEIGHT * 2, Color.Green);
                foreach (Platform platform in platforms) platform.Draw();
                player.Draw();
                Raylib.EndMode2D();

                if (isDying)
                {
                    Raylib.DrawRectangle(0, 0, WIDTH, HEIGHT, new Color(255, 0, 0, 100));
                    Raylib.DrawText("FALLING!", WIDTH / 2 - 50, HEIGHT / 2 - 20, 30, Color.Red);
                }

                Raylib.DrawText("Controls:", 10, 10, 16, Color.Black);
                Raylib.DrawText("A/D or Arrow Keys to move", 10, 30, 12, Color.Black);
                Raylib.DrawText("Space/W/Up to Jump", 10, 45, 12, Color.Black);
                Raylib.DrawText("ESC to Exit", 10, 60, 12, Color.Black);
                string groundedText = player.IsGrounded ? "Grounded" : "Airborne";
                string doubleJumpText = player.CanDoubleJump && !player.IsGrounded ? "Double Jump Available" : "Double Jump Used";
                Raylib.DrawText($"Status: {groundedText}", 10, HEIGHT - 80, 12, Color.Black);
                if (!player.IsGrounded) Raylib.DrawText($"{doubleJumpText}", 10, HEIGHT - 65, 12, Color.Black);
                Raylib.DrawText($"Score: {player.Score:F0}", 10, HEIGHT - 50, 16, Color.Red);
                Raylib.DrawText($"Height: {Math.Max(0, (500 - player.Position.Y)):F0}m", 10, HEIGHT - 30, 12, Color.Black);
                Raylib.DrawText($"Platforms: {platforms.Count}", 10, HEIGHT - 15, 10, Color.Gray);
            }
            else
            {
                Raylib.DrawText("GAME OVER", WIDTH / 2 - 100, HEIGHT / 2 - 50, 40, Color.Red);
                Raylib.DrawText($"Final Score: {player.Score:F0}", WIDTH / 2 - 80, HEIGHT / 2, 20, Color.Black);
                Raylib.DrawText("Press R to restart", WIDTH / 2 - 80, HEIGHT / 2 + 40, 20, Color.DarkGray);
            }

            Raylib.EndDrawing();
        }

        public void Exit() => Raylib.CloseWindow();
    }
}