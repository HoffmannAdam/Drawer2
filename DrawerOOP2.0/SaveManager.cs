using System;
using System.Collections.Generic;
using System.IO;

namespace DrawerOOP2._0
{
    internal class SaveManager
    {
        private static Menu menu = new Menu();

        public void SaveDrawing(List<Drawing> drawings)
        {
            string fileName = "rajz";
            int fileIndex = 1;
            string filePath;

            do
            {
                filePath = $"{fileName}{fileIndex}.txt";
                fileIndex++;
            } while (File.Exists(filePath));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var drawing in drawings)
                {
                    writer.WriteLine($"{drawing.X},{drawing.Y},{drawing.Color}");
                }
            }

            drawings.Clear();
            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine($"The drawing has been saved as '{filePath}'.");
            Console.ReadKey();
        }

        public void LoadDrawing(string filePath, List<Drawing> drawings)
        {
            Console.Clear();
            Console.CursorVisible = false;
            drawings.Clear();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3 &&
                        int.TryParse(parts[0], out int x) &&
                        int.TryParse(parts[1], out int y) &&
                        Enum.TryParse(parts[2], true, out ConsoleColor color))
                    {
                        drawings.Add(new Drawing(x, y, color));
                    }
                }
            }

            foreach (var point in drawings)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.ForegroundColor = point.Color;
                Console.Write('█');
            }

            Console.ResetColor();

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Escape);

            Console.Clear();
            menu.Display();
        }

        public void LoadDrawingsList()
        {
            Console.CursorVisible = false;
            Console.Clear();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "rajz*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("No saved drawings found.");
                Console.ReadKey();
                return;
            }

            int selectedIndex = 0;
            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < files.Length; i++)
                {
                    Console.SetCursorPosition(0, i);
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"> {Path.GetFileName(files[i])}");
                    }
                    else
                    {
                        Console.Write($"  {Path.GetFileName(files[i])}");
                    }

                    Console.ResetColor();
                }

                Console.SetCursorPosition(0, files.Length + 1);
                Console.WriteLine("Select a drawing to load (Press Esc to go back):");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0) selectedIndex--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedIndex < files.Length - 1) selectedIndex++;
                        break;

                    case ConsoleKey.Enter:
                        LoadDrawing(files[selectedIndex], new List<Drawing>());
                        return;
                }

            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
