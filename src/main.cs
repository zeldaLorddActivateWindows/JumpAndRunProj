using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<KeyboardKey> ExitKeys = new List<KeyboardKey>()
        {
            KeyboardKey.Escape, KeyboardKey.Enter
        };

        internal static void Main(string[] args)
        {
            main game = new main();
            game.Init();

            while (!Raylib.WindowShouldClose() && game.isRunning)
            {
                game.Update();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);
                Raylib.EndDrawing();
            }
            game.Exit();
        }

        public void Init()
        {
            Raylib.InitWindow(WIDTH, HEIGHT, NAME);
            Raylib.SetTargetFPS(FPS);
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
        }
        public void Exit() => Raylib.CloseWindow();
        
    }
}