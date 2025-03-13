using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerOOP2._0
{
    internal class DrawingManager
    {
        private static Menu menu = new Menu();
        private static SaveManager saveManager = new SaveManager();
        private List<Drawing> drawings = new List<Drawing>();

        public void StartDrawing()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.Write("1-Fehér 2-Zöld 3-Sárga 4-Piros 5-Kék 6-Cián 7-Magenta Space-Rajzolás Del-Törlés");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);

            int x = 0, y = 0;
            ConsoleColor currentColor = ConsoleColor.White;
            ConsoleKey key;

            do
            {
                Console.SetCursorPosition(x, y);
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (y > 0) y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y < Console.WindowHeight - 2) y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x > 0) x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (x < Console.WindowWidth - 1) x++;
                        break;
                    case ConsoleKey.Spacebar:
                        Console.ForegroundColor = currentColor;
                        Console.Write('█');
                        drawings.Add(new Drawing(x, y, currentColor));
                        break;
                    case ConsoleKey.Delete:
                        Console.Write(" ");
                        drawings.RemoveAll(d => d.X == x && d.Y == y);
                        break;
                }

                switch (key)
                {
                    case ConsoleKey.D1: currentColor = ConsoleColor.White; break;
                    case ConsoleKey.D2: currentColor = ConsoleColor.Green; break;
                    case ConsoleKey.D3: currentColor = ConsoleColor.Yellow; break;
                    case ConsoleKey.D4: currentColor = ConsoleColor.Red; break;
                    case ConsoleKey.D5: currentColor = ConsoleColor.Blue; break;
                    case ConsoleKey.D6: currentColor = ConsoleColor.Cyan; break;
                    case ConsoleKey.D7: currentColor = ConsoleColor.Magenta; break;
                }

                if (Console.CapsLock)
                {
                    Console.ForegroundColor = currentColor;
                    Console.Write('█');
                    drawings.Add(new Drawing(x, y, currentColor));
                }

            } while (key != ConsoleKey.Escape);

            saveManager.SaveDrawing(drawings);

            if (key == ConsoleKey.Escape)
            {
                Console.ResetColor();
                Console.Clear();
                menu.Display();
            }
        }
    }

    internal class Drawing
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }

        public Drawing(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }
}