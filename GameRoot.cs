using System;

namespace GravitySim
{
    public static class GameRoot
    {
        public static MatterManager entityManager = new MatterManager();
        public static Random rand = new Random();

        public static void Initialize()
        {
            
        }

        public static void Update()
        {
            entityManager.UpdateMatter();
            Control.Update();
        }


        public static void Draw()
        {
            entityManager.DrawMatter();
            DrawSpace.WriteToConsole();
        }
    }
}