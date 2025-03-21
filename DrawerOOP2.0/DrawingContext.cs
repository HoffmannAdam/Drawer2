using DrawerOOP2._0;
using Microsoft.EntityFrameworkCore;

public class DrawingContext : DbContext
{
    public DbSet<DrawingEntity> Drawings { get; set; }

    public string DbPath { get; }

    public DrawingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "drawings.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
