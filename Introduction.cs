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
            text.Append("                   GRAVITY SIM                    ");            
            width = text.Length;
            text.Append("                                                  ");
            text.Append("            Press 1 to make new matter            ");
            text.Append("              Use WASD to move camera             ");
            text.Append("            Press C to toggle collision           ");
            text.Append("Use + - to increase and decrease collision rebound");
            text.Append("  Use up/down arrows to adjust the mass of matter ");
            text.Append("             Press R to reset the game            ");
            text.Append("            Expand window for best view           ");
        }

        public static void WriteText()
        {
            char[] arr = text.ToString().ToCharArray();
            int startY = 10;
            int startX = (DrawConsole.drawWidth / 2) - (width / 2);
            int newlineX = startX + width;
            int x = startX;
            int y = startY;

            foreach (var item in arr)
            {
                DrawConsole.DrawCharacter(x, y, item);
                x++;
                if (x >= newlineX) { x = startX; y++; }
            }
        }
    }
}
