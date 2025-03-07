using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerOOP2._0
{
    internal class BorderRenderer
    {
        public void DrawBorder()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.SetCursorPosition(0, 0);
            Console.Write("╔" + new string('═', width - 2) + "╗");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
                Console.SetCursorPosition(width - 1, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(0, height - 1);
            Console.Write("╚" + new string('═', width - 2) + "╝");
        }
    }
}
