namespace DashboardApi.Data;

public class SectionRepo : ISectionRepo
{

    private readonly Context _context;

    public SectionRepo(IConfiguration context)
    {
        _context = new Context(context);
    }

    public async void SaveChanges<T>(T entityToAdd)
    {
        await _context.SaveChangesAsync();
    }

    public void AddEntity<T>(T entityToAdd)
    {
        if (entityToAdd != null)
        {
            _context.Add(entityToAdd);
        }
    }
    public void RemoveEntity<T>(T entityToAdd)
    {
        if (entityToAdd != null)
        {
            _context.Remove(entityToAdd);
        }
    }

}