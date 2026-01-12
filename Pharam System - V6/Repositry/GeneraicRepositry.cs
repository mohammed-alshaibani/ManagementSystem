using Pharam_System___V6.Models;

namespace Pharam_System___V6.Repositry
{
    public class GeneraicRepositry<T> : IRepositry<T> where T : class
    {

        private readonly AppsDbContext _context;
        public GeneraicRepositry(AppsDbContext context)
        {
            _context = context;
        }


        public T Add(T entity)
        {
            try
            {
                var res = _context.Add(entity);
                var saveing = _context.SaveChanges();
                return res.Entity as T;

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public T Delete(int id)
        {
            try
            {
                var res = _context.Remove(id);
                var saveing = _context.SaveChanges();
                return res.Entity as T;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public List<T> GetAll()
        {
            var res = _context.Set<T>().ToList();
            return res;
        }

        public T GetById(int id)
        {
            try
            {
                var res = _context.Set<T>().Find(id);
                return res;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public List<T> GetByUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public List<T> Search(string searchTerm)
        {
            if (_context.Database.CanConnect())
            {
                return _context.Set<T>().AsQueryable();
            }
            else
            {
                return null;
            }

            var query = _context.Set<T>().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Add search conditions here based on the properties of the entity T
                query = query.Where(e => e.Property1.Contains(searchTerm) || e.Property2.Contains(searchTerm));
            }

            return query.ToList();
        }




        public T Update(int Id,T entity)
        {
            try
            {
                var res = _context.Update(entity);
                var saveing = _context.SaveChanges();
                return res.Entity as T;

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
