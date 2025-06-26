namespace JumpAndRun
{
    using Raylib_cs;
    using System.Runtime.InteropServices;
    
    internal class main : IGameWindow
    {
        public string NAME { get; } = "Jump and Run";
        public int WIDTH { get; } = 800;
        public int HEIGHT { get; } = 600;
        public int FPS { get; } = 60;
        private bool isRunning = true;
        private List<KeyboardKey> ExitKeys = new List<KeyboardKey>() { KeyboardKey.Escape, KeyboardKey.Enter };
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
            player.Update(deltaTime);
        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);
            Raylib.DrawRectangle(0, 500, WIDTH, HEIGHT - 500, Color.Green);
            player.Draw();
            Raylib.DrawText("Controls:", 10, 10, 16, Color.Black);
            Raylib.DrawText("A/D or Arrow Keys to move", 10, 30, 12, Color.Black);
            Raylib.DrawText("Space to Jump", 10, 45, 12, Color.Black);
            Raylib.DrawText("ESC or ENTER to Exit", 10, 60, 12, Color.Black);
            
            string groundedText = player.IsGrounded ? "Grounded" : "Airborne";
            string doubleJumpText = player.CanDoubleJump ? "Double Jump Available" : "Double Jump Used";
            Raylib.DrawText($"Status: {groundedText}", 10, HEIGHT - 60, 12, Color.Black);
            Raylib.DrawText($"{doubleJumpText}", 10, HEIGHT - 45, 12, Color.Black);
            Raylib.DrawText($"Score: {player.Score}", 10, HEIGHT - 30, 12, Color.Black);
            
            Raylib.EndDrawing();
        }
        
        public void Exit() => Raylib.CloseWindow();
    }
}

