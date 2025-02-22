namespace Notes.Persistence;

public class DbInitializer
{
    public static void Initialize(NotesesDbContext context)
    {
        context.Database.EnsureCreated();
    }
}