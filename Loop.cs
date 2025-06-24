using System;
using System.Diagnostics;
using System.Threading;

namespace GravitySim
{
    public static class Loop
    {
        public const double TARGET_FPS = 60.0;
        public const double MS_PER_FRAME = 1000.0 / TARGET_FPS;

        public static bool _isRunning;
        private static Stopwatch _gameTimer = new Stopwatch();
        private static double _lag;
        public static int currentFrame = 0;

        public static void Run()
        {
            _gameTimer = Stopwatch.StartNew();
            double previousTime = _gameTimer.Elapsed.TotalMilliseconds;
            _lag = 0.0;
             _isRunning = true;

            GameRoot.Initialize();

            while (_isRunning)
            {
                double currentTime = _gameTimer.Elapsed.TotalMilliseconds;
                double elapsedTime = currentTime - previousTime;
                previousTime = currentTime;
                _lag += elapsedTime;

                while (_lag >= MS_PER_FRAME)
                {
                    if (currentFrame == (int)TARGET_FPS) { currentFrame = 0; }
                    currentFrame++;
                    GameRoot.Update();
                    _lag -= MS_PER_FRAME;
                }

                GameRoot.Draw();

                // Frame rate limiting
                double frameTime = _gameTimer.Elapsed.TotalMilliseconds - currentTime;
                if (frameTime < MS_PER_FRAME)
                {
                    int sleepTime = (int)(MS_PER_FRAME - frameTime);
                    Thread.Sleep(sleepTime);
                }
            }

        }



    }
}