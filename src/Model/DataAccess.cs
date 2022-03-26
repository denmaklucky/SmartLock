namespace Model;

public class DataAccess : IDataAccess, IDisposable
{
    private readonly ApplicationContext _applicationContext;

    public DataAccess(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public void Dispose()
    {
        _applicationContext?.Dispose();
    }
}