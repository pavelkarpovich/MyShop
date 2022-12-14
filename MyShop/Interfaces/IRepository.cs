namespace MyShop.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? GetById(int id);
        void Update(T entity);
        List<T> GetAll();
    }
}
