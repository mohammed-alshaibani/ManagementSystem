using Pharam_System___V6.Models;

namespace Pharam_System___V6.Repositry
{
    public interface IRepositry<T> where T : class
    {
        //Write
        T Add(T entity);
        T Update(int id,T entity);
        T Delete(int id);
        //Read
        T GetById(int id);
        List<T> GetAll();
        List<T> GetByUser(int UserId);
        List<T> Search(string searchTerm);

    }
}
