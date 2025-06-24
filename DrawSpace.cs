using System;
using System.Text;

namespace GravitySim
{
    public static class DrawSpace
    {
        public static int drawWidth = 235;
        public static int drawHeight = 62;
        public static char[] drawArray = new string(' ', drawWidth * drawHeight).ToCharArray();
        public static VectorInt cameraPosition = new VectorInt();


        public static void DrawCharacter(int x, int y, char character)
        {
            int index = ArrayIndex(x - cameraPosition.X, y - cameraPosition.Y);
            if (index >= 0 && index < drawArray.Length)
            {
                drawArray[index] = character;
            }
            
        }

        public static char GetDrawSpace(int x, int y)
        {
            return drawArray[ArrayIndex(x, y)];
        }

        public static int ArrayIndex(int X, int Y)
        {
            return X + (drawWidth * Y);
        }

        public static void CoordsFromIndex(int index, out int X, out int Y)
        {
            X = index % drawWidth;
            Y = index / drawWidth;
        }


        public static void WriteToConsole()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            var sb = new StringBuilder((drawWidth + 1) * drawHeight); // Pre-allocate memory

            for (int y = 0; y < drawHeight; y++)
            {
                for (int x = 0; x < drawWidth; x++)
                {
                    if (x == 0 || x == drawWidth - 1 || y == 0 || y == drawHeight - 1)
                    { sb.Append("O"); }
                    else
                    { sb.Append(drawArray[ArrayIndex(x, y)]); }

                }
                sb.AppendLine(); // Newline after each row
            }

            Console.Write(sb); // Single I/O operation
            drawArray = new string(' ', drawWidth * drawHeight).ToCharArray();
        }
    }
}
