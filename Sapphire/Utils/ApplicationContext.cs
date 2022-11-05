using Microsoft.EntityFrameworkCore;
using Sapphire.MVVM.Model;

namespace Sapphire.Utils;

public class ApplicationContext : DbContext
{
    public DbSet<NoteModel> NoteModels {get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=notes.db");
    }
}