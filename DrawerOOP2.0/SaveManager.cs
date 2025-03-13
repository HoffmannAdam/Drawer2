using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerOOP2._0
{
    internal class SaveManager
    {
        private static Menu menu = new Menu();

        public void SaveDrawing(List<Drawing> drawings)
        {
            string fileName = "rajz1.txt";
            int index = 1;

            while (File.Exists(fileName))
                fileName = $"rajz{++index}.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var d in drawings)
                    writer.WriteLine($"{d.X},{d.Y},{d.Color}");
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine($"A rajz '{fileName}' néven elmentve.");
            Console.ReadKey();
        }

        public void LoadDrawing()
        {
            Console.Clear();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "rajz*.txt");
            if (files.Length == 0)
            {
                Console.WriteLine("Nincsenek mentett rajzok.");
                Console.ReadKey();
                return;
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Válassz egy rajzot (Enter-rel megnyitod, Esc-kilép)");
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < files.Length; i++)
                {
                    Console.SetCursorPosition(0, i);
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("> " + Path.GetFileName(files[i]));
                    }
                    else
                    {
                        Console.Write("  " + Path.GetFileName(files[i]));
                    }
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && selectedIndex > 0) selectedIndex--;
                if (key == ConsoleKey.DownArrow && selectedIndex < files.Length - 1) selectedIndex++;
                if (key == ConsoleKey.Enter) return;
            } while (key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                Console.ResetColor();
                Console.Clear();
                menu.Display();
            }
        }
    }
}