using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravitySim
{
    public static class Introduction
    {
        public static StringBuilder text = new StringBuilder();
        private static int width;

        public static void SetText()
        {
            text.Append("           GRAVITY SIM            ");            
            width = text.Length;
            text.Append("                                  ");
            text.Append("Press numpad 1 to make new matter.");
            text.Append("     Use WASD to move camera.     ");
            text.Append("   Expand window for best view    ");       
        }

        public static void WriteText()
        {
            char[] arr = text.ToString().ToCharArray();
            int startY = 10;
            int startX = (DrawSpace.drawWidth / 2) - (width / 2);
            int newlineX = startX + width;
            int x = startX;
            int y = startY;

            foreach (var item in arr)
            {
                DrawSpace.DrawCharacter(x, y, item);
                x++;
                if (x >= newlineX) { x = startX; y++; }
            }
        }
    }
}
