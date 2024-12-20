using Microsoft.EntityFrameworkCore; //lets us work with database using objects instead of raw sql queries
using database.Models;
public class EncounterPreviewContext : DbContext
{
    public EncounterPreviewContext(DbContextOptions<EncounterPreviewContext> options)
        : base(options)
    {
    }

    public DbSet<EncounterPreview>? EncounterPreviews { get; set; } //EFCore default -> table name = class name pluralized

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map the 'EncounterPreview' model to the 'encounter_preview' table in the database
        modelBuilder.Entity<EncounterPreview>().ToTable("encounter_preview");
    }
}
