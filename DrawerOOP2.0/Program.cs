using Microsoft.EntityFrameworkCore;

namespace DrawerOOP2._0
{
    internal class Program
    {
        private static Menu menu = new Menu();

        static void Main(string[] args)
        {
            EnsureDatabaseCreated();
            menu.Display();
        }

        private static void EnsureDatabaseCreated()
        {
            using (var db = new DrawingContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
