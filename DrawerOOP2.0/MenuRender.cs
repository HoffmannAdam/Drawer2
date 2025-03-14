namespace DrawerOOP2._0
{
    internal class MenuRenderer
    {
        private List<MenuOption> options;
        private BorderRenderer borderRenderer;

        public MenuRenderer(List<MenuOption> options)
        {
            this.options = options;
            borderRenderer = new BorderRenderer();
        }

        public void DrawMenu(int selectedIndex)
        {
            Console.Clear();
            borderRenderer.DrawBorder();
            for (int i = 0; i < options.Count; i++)
            {
                DrawMenuItem(i, i == selectedIndex);
            }
        }

        public void UpdateSelection(int oldIndex, int newIndex)
        {
            if (oldIndex >= 0 && oldIndex < options.Count)
                DrawMenuItem(oldIndex, false);
            DrawMenuItem(newIndex, true);
        }

        private void DrawMenuItem(int index, bool isSelected)
        {
            int windowWidth = Console.WindowWidth;
            int menuWidth = options.Max(opt => opt.Text.Length) + 4;
            int startX = Math.Max(1, (windowWidth - menuWidth) / 2);
            int startY = Math.Max(1, (Console.WindowHeight - options.Count * 3) / 2);

            string text = options[index].Text;
            int y = startY + (index * 3);

            string topBorder = "╔" + new string('═', text.Length + 2) + "╗";
            string middleLine = "║ " + text + " ║";
            string bottomBorder = "╚" + new string('═', text.Length + 2) + "╝";

            Console.SetCursorPosition(startX, y);
            Console.Write(topBorder);

            Console.SetCursorPosition(startX, y + 1);

            if (isSelected)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(middleLine);
                Console.ResetColor();
            }
            else
            {
                Console.Write(middleLine);
            }

            Console.SetCursorPosition(startX, y + 2);
            Console.Write(bottomBorder);
        }
    }
}
