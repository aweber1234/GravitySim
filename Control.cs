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

        public static void AddMatter()
        {
            if (keyPressed == ConsoleKey.NumPad1)
            {
                Vector3 position = 
                    new Vector3(
                        GameRoot.rand.Next(DrawSpace.cameraPosition.X, DrawSpace.cameraPosition.X + DrawSpace.drawWidth), 
                        GameRoot.rand.Next(DrawSpace.cameraPosition.Y, DrawSpace.cameraPosition.Y + DrawSpace.drawHeight), 0);

                GameRoot.entityManager.Add(new Matter(0.1f, position, new Vector3(0, 0, 0)));

            }
        }

        public static void MoveCamera()
        {
            if (keyPressed == ConsoleKey.W)
            {
                DrawSpace.cameraPosition.Y -= 2;
            }
            else if (keyPressed == ConsoleKey.S)
            {
                DrawSpace.cameraPosition.Y += 2;
            }
            else if (keyPressed == ConsoleKey.D)
            {
                DrawSpace.cameraPosition.X += 2;
            }
            else if (keyPressed == ConsoleKey.A)
            {
                DrawSpace.cameraPosition.X -= 2;
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


                AddMatter();
                MoveCamera();
            }
        }
    }
}