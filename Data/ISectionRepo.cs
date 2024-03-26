namespace DashboardApi.Data
{
    public interface ISectionRepo
    {
        public void SaveChanges<T>(T entityToAdd);
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToAdd);
    }
}