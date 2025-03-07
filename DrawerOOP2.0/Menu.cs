using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrawerOOP2._0
{
    internal class Menu
    {
        private List<MenuOption> options;
        private int index = 0;
        private MenuRenderer renderer;

        public Menu()
        {
            options = new List<MenuOption>
            {
                new MenuOption("Új", () => new DrawingManager().StartDrawing()),
                new MenuOption("Mentett rajzok", () => new SaveManager().LoadDrawing()),
                new MenuOption("Törlés", () => new DeleteManager().DeleteDrawing()),
                new MenuOption("Kilépés", () => Environment.Exit(0))
            };

            renderer = new MenuRenderer(options);
        }

        public void Display()
        {
            Console.CursorVisible = false;
            ConsoleKeyInfo key;
            renderer.DrawMenu(index);

            do
            {
                key = Console.ReadKey(true);
                int oldIndex = index;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index > 0) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index < options.Count - 1) index++;
                        break;
                    case ConsoleKey.Enter:
                        options[index].Execute();
                        break;
                }

                renderer.UpdateSelection(oldIndex, index);

            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
