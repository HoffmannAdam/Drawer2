using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerOOP2._0
{
    internal class DeleteManager
    {
        private static Menu menu = new Menu();

        public void DeleteDrawing()
        {
            int selectedIndex = 0;
            ConsoleKeyInfo key;
            Console.Clear();
            Console.CursorVisible = false;
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "rajz*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("No saved drawings found.");
                Console.ReadKey();
                menu.Display();
                return;
            }


            for (int i = 0; i < files.Length; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write($"  {Path.GetFileName(files[i])}");
            }

            do
            {
                Console.SetCursorPosition(0, selectedIndex);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"> {Path.GetFileName(files[selectedIndex])}");
                Console.ResetColor();

                key = Console.ReadKey(true);

                Console.SetCursorPosition(0, selectedIndex);
                Console.Write($"  {Path.GetFileName(files[selectedIndex])} ");

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0) selectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedIndex < files.Length - 1) selectedIndex++;
                        break;
                    case ConsoleKey.Enter:
                        File.Delete(files[selectedIndex]);
                        Console.SetCursorPosition(0, files.Length + 1);
                        Console.WriteLine($"The drawing {Path.GetFileName(files[selectedIndex])} has been deleted.");
                        Console.ReadKey();
                        menu.Display();
                        return;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        menu.Display();
                        break;
                }

            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
