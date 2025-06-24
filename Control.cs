using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GravitySim
{
    public static class Control
    {
        private static ConsoleKey keyPressed;
        private static int moveSpeed = 5;

        public static void DrawControlStatus()
        {
            DrawConsole.WriteTextLine($"Collision Enabled: {GameRoot.collisionOn}", 3, 2);
            DrawConsole.WriteTextLine($"Collision Rebound: {GameRoot.collisionRebound}", 3, 3);
        }


        public static void ResetGame()
        {
            if (keyPressed == ConsoleKey.R)
            {
                Gravity.forceHash.Clear();
                GameRoot.entityManager.matterObjects.Clear();
                GameRoot.entityManager.matterObjectsSpace.Clear();
                GameRoot.collisionRebound = 0.8f;
                GameRoot.collisionOn = true;
                DrawConsole.cameraPosition = new VectorInt(0, 0, 0);
                DrawConsole.drawArray = new string(' ', DrawConsole.drawWidth * DrawConsole.drawHeight).ToCharArray();
                GameRoot.hasStartedSim = false;               

            }
        }


        public static void AddMatter()
        {
            if (keyPressed == ConsoleKey.D1 || keyPressed == ConsoleKey.NumPad1)
            {
                Vector3 position =
                    new Vector3(
                        GameRoot.rand.Next(DrawConsole.cameraPosition.X, DrawConsole.cameraPosition.X + DrawConsole.drawWidth),
                        GameRoot.rand.Next(DrawConsole.cameraPosition.Y, DrawConsole.cameraPosition.Y + DrawConsole.drawHeight), 0);

                GameRoot.entityManager.Add(new Matter(0.05f, position, new Vector3(0, 0, 0)));

            }
        }

        public static void ModifyCollision()
        {
            if (keyPressed == ConsoleKey.OemPlus || keyPressed == ConsoleKey.Add)
            {
                GameRoot.collisionRebound = Math.Min(1, (float)Math.Round(GameRoot.collisionRebound + 0.05f, 2));
            }
            if (keyPressed == ConsoleKey.OemMinus || keyPressed == ConsoleKey.Subtract) 
            {
                GameRoot.collisionRebound = Math.Max(0, (float)Math.Round(GameRoot.collisionRebound - 0.05f,2));
            }
        }

        public static void ToggleCollision()
        {
            if (keyPressed == ConsoleKey.C)
            {
                GameRoot.collisionOn = !GameRoot.collisionOn;
            }
        }

        public static void MoveCamera()
        {
            if (keyPressed == ConsoleKey.W)
            {
                DrawConsole.cameraPosition.Y -= moveSpeed;
            }
            else if (keyPressed == ConsoleKey.S)
            {
                DrawConsole.cameraPosition.Y += moveSpeed;
            }
            else if (keyPressed == ConsoleKey.D)
            {
                DrawConsole.cameraPosition.X += moveSpeed;
            }
            else if (keyPressed == ConsoleKey.A)
            {
                DrawConsole.cameraPosition.X -= moveSpeed;
            }
        }



        public static void Update()
        {
            // Handle input
            if (Console.KeyAvailable)
            {
                if (!GameRoot.hasStartedSim) { GameRoot.hasStartedSim = true; }

                keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.Escape)
                    Loop._isRunning = false;

                ResetGame();
                ModifyCollision();
                ToggleCollision();
                AddMatter();
                MoveCamera();

            }
        }
    }
}