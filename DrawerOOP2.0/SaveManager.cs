namespace DrawerOOP2._0
{
    internal class SaveManager
    {
        private static Menu menu = new Menu();

        public void SaveDrawing(List<Drawing> drawings)
        {
            using (var context = new DrawingContext())
            {
                var drawingEntities = drawings.Select(d => new DrawingEntity
                {
                    X = d.X,
                    Y = d.Y,
                    Color = d.Color.ToString()
                }).ToList();

                context.Drawings.AddRange(drawingEntities);
                context.SaveChanges();
            }
        }

        public void LoadDrawing(int drawingId, List<Drawing> drawings)
        {
            using (var context = new DrawingContext())
            {
                var drawingEntities = context.Drawings.Where(d => d.Id == drawingId).ToList();

                drawings.Clear();
                foreach (var entity in drawingEntities)
                {
                    if (Enum.TryParse(entity.Color, out ConsoleColor color))
                    {
                        drawings.Add(new Drawing(entity.X, entity.Y, color));
                    }
                }
            }
        }

        public void LoadDrawingsList()
        {
            Console.CursorVisible = false;
            Console.Clear();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "rajz*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("Nincsenek mentett rajzok.");
                Console.ReadKey();
                menu.Display();
                return;
            }

            int selectedIndex = 0;
            ConsoleKeyInfo key;

            do
            {
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
                Console.WriteLine("Válassz ki egy rajzot a betöltéshez (Nyomj Esc-et a kilépéshez):");

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
                    case ConsoleKey.Escape:
                        Console.Clear();
                        menu.Display();
                        break;
                }

            } while (key.Key != ConsoleKey.Escape);
        }

        private void LoadDrawing(string v, List<Drawing> drawings)
        {
            throw new NotImplementedException();
        }

        public List<int> GetAllDrawingIds()
        {
            using (var context = new DrawingContext())
            {
                return context.Drawings.Select(d => d.Id).ToList();
            }
        }
    }
}
