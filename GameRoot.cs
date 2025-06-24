using System;
using System.Runtime.InteropServices;

namespace GravitySim
{
    public static class GameRoot
    {
        public static MatterManager entityManager = new MatterManager();
        public static Random rand = new Random();
        public static bool hasStartedSim = false;

        public static void Initialize()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                
            }

            Introduction.SetText();
        }

        public static void Update()
        {
            entityManager.UpdateMatter();
            Control.Update();
        }


        public static void Draw()
        {
            if (!hasStartedSim) { Introduction.WriteText(); }
            entityManager.DrawMatter();
            DrawSpace.WriteToConsole();
        }
    }
}